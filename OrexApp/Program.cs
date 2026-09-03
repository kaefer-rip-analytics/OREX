using Microsoft.EntityFrameworkCore;
using OrexApp.Infra.Banco;

using OrexApp.ManterUsuario.Features.IUsuarioRepository;
using OrexApp.ManterUsuario.Features.IUsuarioService;
using OrexApp.ManterUsuario.Features.UsuarioRepository;
using OrexApp.ManterUsuario.Features.UsuarioService;

var builder = WebApplication.CreateBuilder(args);

// Banco de Dados
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    .LogTo(Console.WriteLine, LogLevel.Information)
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors());

// Injeção de Dependência
builder.Services.AddScoped<IUsuariosRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuariosService, UsuarioService>();

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
var allowedOrigins =
    builder.Configuration
        .GetSection("Cors:AllowedOrigins")
        .Get<string[]>() ?? [];

var cloudflareOrigin = "https://washing-seeker-tracks-gem.trycloudflare.com";

var origins = allowedOrigins
    .Append(cloudflareOrigin)
    .ToArray();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEnd", policy =>
    {
        policy.WithOrigins(origins)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
    });
});

var app = builder.Build();

// Middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrexApp API v1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseCors("FrontEnd");
app.MapControllers();

app.Run();