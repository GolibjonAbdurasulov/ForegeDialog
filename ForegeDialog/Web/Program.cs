using System.Net;
using System.Net.Mail;
using System.Text;
using System.IO.Compression;
using DatabaseBroker;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using Services.Services;
using Web.Extension;
using Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Konfiguratsiya fayllarini yuklash
builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

// DbContext
builder.Services.AddDbContextPool<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionString"));
    options.UseLazyLoadingProxies();
});

// Kestrel va MaxRequestBodySize
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080);
    options.Limits.MaxRequestBodySize = 314572800; // 300 MB
});

// JWT autentifikatsiyasi
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    string key = builder.Configuration["Jwt:SecurityKey"];
    string issuer = builder.Configuration["Jwt:Issuer"];
    string audience = builder.Configuration["Jwt:Audience"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = issuer,
        ValidateAudience = true,
        ValidAudience = audience,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
        ClockSkew = TimeSpan.Zero
    };
});

// Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "https://foragedialog.uz", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

// Response Compression (Gzip)
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<GzipCompressionProvider>();
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
    {
        "application/json",
        "text/plain",
        "text/html",
        "application/javascript",
        "text/css"
    });
});
builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});

// Controllers va servislar
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureRepositories();
builder.Services.AddMemoryCache();

// SMTP
var smtpConfig = builder.Configuration.GetSection("Smtp");
builder.Services.AddScoped<SmtpClient>(sp =>
{
    return new SmtpClient(smtpConfig["Host"], int.Parse(smtpConfig["Port"]))
    {
        Credentials = new NetworkCredential(smtpConfig["User"], smtpConfig["Password"]),
        EnableSsl = true
    };
});

// Custom servislar
builder.Services.ConfigureServicesFromTypeAssembly<OtpService>();
builder.Services.ConfigureServicesFromTypeAssembly<UserService>();
builder.Services.ConfigureServicesFromTypeAssembly<AuthService>();
builder.Services.ConfigureServicesFromTypeAssembly<FileService>();
builder.Services.ConfigureServicesFromTypeAssembly<TranslationService>();
builder.Services.ConfigureServicesFromTypeAssembly<TokenService>();
builder.Services.ConfigureServicesFromTypeAssembly<EmailNotificationService>();

// Global Exception Middleware
builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();

var app = builder.Build();

// Middleware
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseResponseCompression(); // Gzip
app.UseRouting();
app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
    });
}

// Endpoints
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// Npgsql JSON qo‘llab-quvvatlash
NpgsqlConnection.GlobalTypeMapper.EnableDynamicJson();

// Ishga tushirish
app.Run();
