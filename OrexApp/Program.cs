using System.Text;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

using OrexApp.Infra.Banco;

using OrexApp.Features.ManterUsuario.Usuario;
using OrexApp.Features.ManterUsuario.IUsuarioRepository;
using OrexApp.Features.ManterUsuario.IUsuarioService;
using OrexApp.Features.ManterUsuario.UsuarioRepository;
using OrexApp.Features.ManterUsuario.UsuarioService;

using OrexApp.Features.ManterProduto.IProdutoRepository;
using OrexApp.Features.ManterProduto.IProdutoService;
using OrexApp.Features.ManterProduto.ProdutoRepository;
using OrexApp.Features.ManterProduto.ProdutoService;

using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// =====================================================
// Configurações
// =====================================================

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("ConnectionStrings:DefaultConnection não foi configurada.");
}

var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrWhiteSpace(jwtKey))
{
    throw new InvalidOperationException("Jwt:Key não foi configurada.");
}

var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "OrexApp";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "OrexFront";

var allowedOrigins =
    builder.Configuration
        .GetSection("Cors:AllowedOrigins")
        .Get<string[]>() ?? [];

var cloudflareOrigin = "https://bean-wiring-fine-principles.trycloudflare.com";

var origins = allowedOrigins
    .Append(cloudflareOrigin)
    .ToArray();

// =====================================================
// Serilog
// =====================================================

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithEnvironmentName()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

// =====================================================
// Banco de dados
// =====================================================

builder.Services.AddDbContext<ApplicationDbContext>(
    options =>{
        options.UseSqlServer(connectionString);
    });

builder.Services
    .AddIdentityCore<Usuarios>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

// =====================================================
// CORS
// =====================================================

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEnd", policy =>
    {
        policy.WithOrigins(origins)
                .AllowAnyHeader()
                .AllowAnyMethod();
    });
});

// =====================================================
// JWT Authentication
// =====================================================

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtIssuer,
                ValidAudience = jwtAudience,

                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),

                ClockSkew = TimeSpan.FromMinutes(1)
            };
    });

// =====================================================
// Política global de autenticação
// =====================================================

builder.Services
    .AddAuthorizationBuilder()
    .SetFallbackPolicy(
        new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build());

// =====================================================
// Health Check
// =====================================================

builder.Services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>(name: "sqlserver");

// =====================================================
// Rate Limiting
// =====================================================

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddFixedWindowLimiter(
    "Login",
    limiterOptions =>
    {
        limiterOptions.PermitLimit = 5;
        limiterOptions.Window =
            TimeSpan.FromMinutes(1);
        limiterOptions.QueueLimit = 0;
    });

    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        {
            var key = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

            return RateLimitPartition.GetFixedWindowLimiter(key, _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 100,
                        Window = TimeSpan.FromMinutes(1),
                        QueueLimit = 0,
                        QueueProcessingOrder =
                            QueueProcessingOrder.OldestFirst
                    });
        });

    options.OnRejected = async (context, cancellationToken) =>
    {
        context.HttpContext.Response.ContentType = "application/json";

        await context.HttpContext.Response.WriteAsJsonAsync( new{ mensagem = "Muitas requisições. Tente novamente mais tarde." }, cancellationToken );
    };
});

// =====================================================
// OpenTelemetry
// =====================================================

var otlpEndpoint = builder.Configuration["OpenTelemetry:OtlpEndpoint"];

var openTelemetryBuilder = builder.Services.AddOpenTelemetry().ConfigureResource(resource =>
        {
            resource.AddService(
                serviceName: "OrexApp",
                serviceVersion: "1.0.0");
        })
        .WithTracing(tracing =>
        {
            tracing.AddAspNetCoreInstrumentation();
            if (!string.IsNullOrWhiteSpace(otlpEndpoint))
            {
                tracing.AddOtlpExporter(options =>
                {
                    options.Endpoint = new Uri(otlpEndpoint);
                });
            }
        })
        .WithMetrics(metrics =>
        {
            metrics.AddAspNetCoreInstrumentation().AddRuntimeInstrumentation();
        });

// =====================================================
// Injeção de dependências
// =====================================================

builder.Services.AddScoped<IUsuariosRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuariosService, UsuarioService>();

builder.Services.AddScoped<IProdutosRepository, ProdutoRepository>();
builder.Services.AddScoped<IProdutosService, ProdutoService>();

// =====================================================
// Controllers e Swagger
// =====================================================

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
       options.JsonSerializerOptions.Converters.Add(
        new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// =====================================================
// Pipeline HTTP
// =====================================================

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors("FrontEnd");

app.UseRateLimiter();

app.UseAuthentication();

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

// Health Check público
app.MapHealthChecks("/health").AllowAnonymous();

app.MapControllers();

app.Run();