using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using qwikhr.Common;

namespace qwikhr.Interceptors
{
    public class CompanyIdInterceptor : SaveChangesInterceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CompanyIdInterceptor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;

            if (context == null)
                return base.SavingChangesAsync(eventData, result, cancellationToken);

            var user = _httpContextAccessor.HttpContext?.User;
            if (user != null)
            {
                var companyIdClaim = user.Claims.FirstOrDefault(c => c.Type == "CompanyId")?.Value;
                if (Guid.TryParse(companyIdClaim, out var companyId))
                {
                    // Loop through all entities that inherit from CompanyEntity
                    foreach (var entry in context.ChangeTracker.Entries<CompanyEntity>())
                    {
                        if (entry.State == EntityState.Added && entry.Entity.CompanyId == Guid.Empty)
                        {
                            entry.Entity.CompanyId = companyId;
                        }
                    }
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}

