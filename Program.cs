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

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();
Env.Load();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter());
});

var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbUser = Environment.GetEnvironmentVariable("DB_USER");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");


var connectionString = $"Server={dbHost};Database={dbName};User Id={dbUser};Password={dbPassword};";
Console.WriteLine(connectionString);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

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

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("https://app.qwikhr.com", "http://localhost:5173") // Replace with your frontend domain
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// reositories
builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IRegionRepository, RegionRepository>();

// services
builder.Services.AddScoped<CreateAdminAccountService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddFluentEnail();

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
}
app.UseCors("AllowSpecificOrigins");
app.ApplyMigrations();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
