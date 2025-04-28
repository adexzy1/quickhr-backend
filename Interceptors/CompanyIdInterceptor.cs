using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using qwikhr.Common;

namespace qwikhr.Interceptors
{
    public class CompanyIdInterceptor : SaveChangesInterceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CompanyIdInterceptor> _logger;

        public CompanyIdInterceptor(IHttpContextAccessor httpContextAccessor,
            ILogger<CompanyIdInterceptor> logger)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
     DbContextEventData eventData,
     InterceptionResult<int> result,
     CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;
            if (context == null) return await base.SavingChangesAsync(eventData, result, cancellationToken);

            try
            {
                // Skip if no HTTP context (e.g., background jobs)
                var companyIdClaim = _httpContextAccessor.HttpContext?.User?
                    .FindFirst("CompanyId")?.Value;

                if (Guid.TryParse(companyIdClaim, out var companyId))
                {
                    foreach (var entry in context.ChangeTracker.Entries<CompanyEntity>())
                    {
                        // Only set CompanyId if it's truly new and unset
                        if (entry.State == EntityState.Added && entry.Entity.CompanyId == Guid.Empty)
                        {
                            entry.Entity.CompanyId = companyId;
                            entry.Entity.Version++;  // Increment version if using concurrency control
                        }
                    }
                }

                return await base.SavingChangesAsync(eventData, result, cancellationToken);
            }
            catch (Exception ex)
            {
                // Log the error but don't block saving
                _logger.LogWarning(ex, "CompanyId interceptor failed");
                return await base.SavingChangesAsync(eventData, result, cancellationToken);
            }
        }
    }
}

