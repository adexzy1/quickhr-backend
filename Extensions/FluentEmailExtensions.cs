namespace qwikhr.Extensions
{
    public static class FluentEmailExtensions
    {
        public static void AddFluentEnail(this IServiceCollection services)
        {
            var defaultFromEmail = Environment.GetEnvironmentVariable("DEFAULT_FROM");
            var host = Environment.GetEnvironmentVariable("SMTP_HOST");
            var port = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT") ?? "0");
            var username = Environment.GetEnvironmentVariable("SMTP_USERNAME");
            var password = Environment.GetEnvironmentVariable("SMTP_PASSWORD");
            services.AddFluentEmail(defaultFromEmail)
            .AddSmtpSender(host, port, username, password)
            .AddRazorRenderer();
        }
    }
}