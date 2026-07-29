using CampusHire.API.Authentication;
using CampusHire.API.Data;
using CampusHire.API.Helpers;
using CampusHire.API.Interfaces;
using CampusHire.API.Mappings;
using CampusHire.API.Middleware;
using CampusHire.API.Models;
using CampusHire.API.Repositories.Implementations;
using CampusHire.API.Repositories.Interfaces;
using CampusHire.API.Services.Implementations;
using CampusHire.API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/campushire-log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddDbContext<CampusHireDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<JwtService>();

builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();

builder.Services.AddScoped<IPlacementDriveRepository, PlacementDriveRepository>();
builder.Services.AddScoped<IPlacementDriveService, PlacementDriveService>();

builder.Services.AddScoped<IStudentVerificationRepository, StudentVerificationRepository>();
builder.Services.AddScoped<IStudentVerificationService, StudentVerificationService>();

builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IAdminService, AdminService>();

builder.Services.AddScoped<IEligibilityCriteriaRepository, EligibilityCriteriaRepository>();
builder.Services.AddScoped<IEligibilityCriteriaService, EligibilityCriteriaService>();

builder.Services.AddScoped<IAdminActivityRepository, AdminActivityRepository>();
builder.Services.AddScoped<IAdminActivityService, AdminActivityService>();

builder.Services.AddScoped<ActivityLogger>();
builder.Services.AddScoped<IExportService, ExportService>();

builder.Services.AddScoped<IPasswordResetService, PasswordResetService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<IRegistrationDeadlineRepository, RegistrationDeadlineRepository>();
builder.Services.AddScoped<IRegistrationDeadlineService, RegistrationDeadlineService>();
builder.Services.AddScoped<IEmailVerificationService, EmailVerificationService>();
builder.Services.AddScoped<IOtpService, OtpService>();
builder.Services.AddScoped<ISmsService, SmsService>();
builder.Services.AddScoped<IPlacementResultRepository, PlacementResultRepository>();
builder.Services.AddScoped<IPlacementResultService, PlacementResultService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CampusHire Admin API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT Token"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CampusHireDbContext>();
    DataSeeder.Seed(context);
}

app.Run();