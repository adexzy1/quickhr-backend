namespace qwikhr.helper
{

    public class UserContextHelper(IHttpContextAccessor httpContextAccessor)
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public Guid? GetUserCompanyIdOrNull()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null) return null;

            var user = httpContext.User;

            // If the user is a super admin, return null to disable filtering
            if (user.IsInRole("SuperAdmin"))
            {
                return null;
            }

            var companyIdClaim = user.Claims.FirstOrDefault(c => c.Type == "CompanyId")?.Value;
            return companyIdClaim != null ? Guid.Parse(companyIdClaim) : null;
        }

        public bool CanAccessCompany(Guid entityCompanyId)
        {
            Guid? userCompanyId = GetUserCompanyIdOrNull();

            // If the user is a super admin, allow access
            if (userCompanyId == null)
            {
                return true;
            }

            return userCompanyId == entityCompanyId;
        }

    }

}