using Microsoft.EntityFrameworkCore;
using qwikhr.Data;
using DotNetEnv;
using qwikhr.Interfaces;
using qwikhr.Repository;
using qwikhr.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using qwikhr.Services;
using Microsoft.AspNetCore.Mvc.Authorization;
using qwikhr.Extensions;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Slack;
using qwikhr.Middleware;
using qwikhr.Interceptors;
using qwikhr.helper;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();
Env.Load();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("https://app.qwikhr.com", "http://localhost:5173")
                    .AllowCredentials() // Allow requests from any origin
                   .AllowAnyMethod() // Allow all HTTP methods (GET, POST, PUT, DELETE, OPTIONS, etc.)
                   .AllowAnyHeader(); // Allow all headers
        });
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter());
}).AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbUser = Environment.GetEnvironmentVariable("DB_USER");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");


var connectionString = $"Server={dbHost};Database={dbName};User Id={dbUser};Password={dbPassword};";

builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
{
    options.UseNpgsql(connectionString).AddInterceptors(sp.GetRequiredService<CompanyIdInterceptor>());
});

// Authentication
builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireUppercase = true;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = true;
}).AddEntityFrameworkStores<ApplicationDbContext>();

// Configure Serilog with Slack sink
Log.Logger = new LoggerConfiguration()
    .WriteTo.Slack(
        webhookUrl: Environment.GetEnvironmentVariable("SLACK_WEBHOOK_URL"),
        restrictedToMinimumLevel: LogEventLevel.Error,
        period: TimeSpan.FromSeconds(5),
        customChannel: Environment.GetEnvironmentVariable("SLACK_CHANNEL"),
        customUsername: Environment.GetEnvironmentVariable("SLACK_USERNAME"),
        customIcon: ":ghost:")
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
      options.DefaultChallengeScheme =
      options.DefaultForbidScheme =
      options.DefaultScheme =
      options.DefaultSignInScheme =
      options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
           System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"] ?? throw new InvalidOperationException("JWT SigningKey is not configured"))
       )
    };
    // global error message for 401 response
    options.Events = new JwtBearerEvents
    {
        OnChallenge = async context =>
        {
            context.HandleResponse();
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            var responseObj = new
            {
                message = "Unauthorized access"
            };
            await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(responseObj));
        }
    };
});


// reositories
builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IPayGradeRepository, PayGradeRepository>();
builder.Services.AddScoped<IPayComponentRepository, PayComponentRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeePayComponentRepository, EmployeePayComponentRepository>();
builder.Services.AddScoped<IPayrollPeriodRepository, PayrollPeriodRepository>();
builder.Services.AddScoped<IPayrollEntryRepository, PayrollEntryRepository>();
builder.Services.AddScoped<IPayrollRunRepository, PayrollRunRepository>();
builder.Services.AddScoped<IPayrollApprovalRepository, PayrollApprovalRepository>();
builder.Services.AddScoped<IPayrollApprovalHistoryRepository, PayrollApprovalHistoryRepository>();


// services
builder.Services.AddScoped<CreateAdminAccountService>();
builder.Services.AddScoped<CreateEmployeeService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddFluentEnail();
builder.Services.AddScoped<IOtpService, OtpService>();
builder.Services.AddSingleton<JwtCookieService>();
builder.Services.AddScoped<UserContextHelper>();
builder.Services.AddScoped<PayrollService>();
builder.Services.AddScoped<PayrollApprovalService>();
builder.Services.AddScoped<PayrollRunRepository>();

//interceptors
builder.Services.AddScoped<CompanyIdInterceptor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
if (app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
    app.ApplyMigrations();
}

app.UseCors("AllowSpecificOrigins");
app.Use(async (context, next) =>
{
    if (context.Request.Method == "OPTIONS")
    {
        context.Response.StatusCode = 204;
        context.Response.Headers.Append("Access-Control-Allow-Origin", context.Request.Headers.Origin);
        context.Response.Headers.Append("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
        context.Response.Headers.Append("Access-Control-Allow-Headers", "Content-Type, Authorization");
        context.Response.Headers.Append("Access-Control-Allow-Credentials", "true");
        await context.Response.CompleteAsync();
        return;
    }
    await next();
});
app.UseMiddleware<JwtCookieOrHeaderMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
