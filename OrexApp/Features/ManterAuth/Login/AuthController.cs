using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;

using OrexApp.Features.ManterAuth.Login.DTOs.LoginRequest;
using OrexApp.Features.ManterAuth.Login.DTOs.LoginResponse;
using OrexApp.Features.ManterUsuario.Usuario;

namespace OrexApp.Features.ManterAuth.Login.AuthController;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<Usuarios> _userManager;
    private readonly SignInManager<Usuarios> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthController(
        UserManager<Usuarios> userManager,
        SignInManager<Usuarios> signInManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [AllowAnonymous]
    [EnableRateLimiting("Login")]
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(
        [FromBody] LoginRequest request)
    {
        var usuario =
            await _userManager.FindByEmailAsync(request.Email);

        if (usuario is null || !usuario.Ativo)
        {
            return Unauthorized(new
            {
                mensagem = "E-mail ou senha inválidos."
            });
        }

        var resultado =
            await _signInManager.CheckPasswordSignInAsync(
                usuario,
                request.Password,
                lockoutOnFailure: true);

        if (!resultado.Succeeded)
        {
            return Unauthorized(new
            {
                mensagem = "E-mail ou senha inválidos."
            });
        }

        var roles =
            await _userManager.GetRolesAsync(usuario);

        var token = CriarToken(usuario, roles);

        return Ok(new LoginResponse(
            token,
            usuario.Id,
            usuario.Nome,
            usuario.Email ?? string.Empty,
            roles));
    }

    private string CriarToken(
        Usuarios usuario,
        IList<string> roles)
    {
        var claims = new List<Claim>
        {
            new(
                JwtRegisteredClaimNames.Sub,
                usuario.Id),

            new(
                JwtRegisteredClaimNames.Email,
                usuario.Email ?? string.Empty),

            new(
                ClaimTypes.Name,
                usuario.UserName ?? usuario.Email ?? usuario.Id)
        };

        foreach (var role in roles)
        {
            claims.Add(
                new Claim(ClaimTypes.Role, role));
        }

        var jwtKey = _configuration["Jwt:Key"];

        if (string.IsNullOrWhiteSpace(jwtKey))
        {
            throw new InvalidOperationException(
                "Jwt:Key não foi configurada.");
        }

        var signingKey =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey));

        var credentials =
            new SigningCredentials(
                signingKey,
                SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}