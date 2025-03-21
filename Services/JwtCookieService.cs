namespace qwikhr.Services
{
    public class JwtCookieService
    {
        private const string CookieName = "accessToken";
        public void SetJwtCookie(HttpContext httpContext, string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(7),
            };

            httpContext.Response.Cookies.Append(CookieName, token, cookieOptions);
        }

        public void RemoveJwtCookie(HttpContext httpContext)
        {
            if (httpContext.Request.Cookies.ContainsKey(CookieName))
            {
                httpContext.Response.Cookies.Delete(CookieName);
            }
        }

    }
}