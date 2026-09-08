using Microsoft.AspNetCore.Identity;
using OrexApp.Features.ManterUsuario;
using OrexApp.Features.ManterUsuario.Usuario;

namespace OrexApp.Infra.Identity;

public static class IdentitySeed
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Usuarios>>();

        var roleManager =scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        foreach (var perfil in Enum.GetValues<UsuarioPerfil>())
        {
            var roleName = perfil.ToString();

            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));

                if (!roleResult.Succeeded)
                {
                    throw new InvalidOperationException(
                        string.Join(
                            "; ",
                            roleResult.Errors.Select(
                                error => error.Description)));
                }
            }
        }

        var adminEmail = configuration["Identity:AdminEmail"];

        var adminPassword = configuration["Identity:AdminPassword"];

        if (string.IsNullOrWhiteSpace(adminEmail) || string.IsNullOrWhiteSpace(adminPassword))
        {
            throw new InvalidOperationException("Identity:AdminEmail e Identity:AdminPassword não foram configurados.");
        }

        var email = adminEmail.Trim().ToLowerInvariant();

        var admin = await userManager.FindByEmailAsync(email);

        if (admin is not null)
        {
            return;
        }

        admin = new Usuarios
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true,
            Nome = "Administrador",
            Perfil = UsuarioPerfil.Administrador,
            Ativo = true,
            DtCadastro = DateTime.UtcNow
        };

        var userResult = await userManager.CreateAsync(admin, adminPassword);

        if (!userResult.Succeeded)
        {
            throw new InvalidOperationException(
                string.Join(
                    "; ",
                    userResult.Errors.Select(
                        error => error.Description)));
        }

        var roleResultAdmin = await userManager.AddToRoleAsync(admin, UsuarioPerfil.Administrador.ToString());

        if (!roleResultAdmin.Succeeded)
        {
            throw new InvalidOperationException(
                string.Join(
                    "; ",
                    roleResultAdmin.Errors.Select(
                        error => error.Description)));
        }
    }
}