namespace qwikhr.Middleware
{
    public class JwtCookieOrHeaderMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context)
        {
            string? token = null;

            // Try to get JWT from Authorization header (for mobile)
            if (context.Request.Headers.TryGetValue("Authorization", out Microsoft.Extensions.Primitives.StringValues value))
            {
                var authHeader = value.ToString();
                if (authHeader.StartsWith("Bearer "))
                {
                    token = authHeader["Bearer ".Length..].Trim();
                }
            }
            else
            {
                // Try to get JWT from Cookies (for web)
                context.Request.Cookies.TryGetValue("accessToken", out token);
            }

            if (!string.IsNullOrEmpty(token))
            {
                context.Request.Headers["Authorization"] = "Bearer " + token;
            }

            await _next(context);
        }
    }

}