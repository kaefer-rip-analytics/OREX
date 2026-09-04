using Microsoft.EntityFrameworkCore;
using OrexApp.Infra.Banco;

using OrexApp.Features.ManterUsuario.IUsuarioRepository;
using OrexApp.Features.ManterUsuario.IUsuarioService;
using OrexApp.Features.ManterUsuario.UsuarioRepository;
using OrexApp.Features.ManterUsuario.UsuarioService;

using OrexApp.Features.ManterProduto.IProdutoRepository;
using OrexApp.Features.ManterProduto.IProdutoService;
using OrexApp.Features.ManterProduto.ProdutoRepository;
using OrexApp.Features.ManterProduto.ProdutoService;

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

builder.Services.AddScoped<IProdutosRepository, ProdutoRepository>();
builder.Services.AddScoped<IProdutosService, ProdutoService>();

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

var cloudflareOrigin = "https://bean-wiring-fine-principles.trycloudflare.com";

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