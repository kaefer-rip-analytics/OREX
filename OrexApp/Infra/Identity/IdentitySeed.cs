using Microsoft.AspNetCore.Identity;
using OrexApp.Features.MantainUser;
using OrexApp.Features.MantainUser.User;

namespace OrexApp.Infra.Identity;

public static class IdentitySeed
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Users>>();

        var roleManager =scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        foreach (var roles in Enum.GetValues<UserRoles>())
        {
            var roleName = roles.ToString();

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

        admin = new Users
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true,
            Nome = "Administrador",
            Roles = UserRoles.Administrador,
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

        var roleResultAdmin = await userManager.AddToRoleAsync(admin, UserRoles.Administrador.ToString());

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