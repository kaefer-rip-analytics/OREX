using OrexApp.Features.MantainUser.User;
using OrexApp.Features.MantainUser.Roles;

namespace OrexApp.Features.MantainUser.DTOs.UserResponse;

public record UsersResponse(
    string Id, 
    string Nome, 
    string Email, 
    UserRoles Roles, 
    bool Ativo,
    DateTime? DtCadastro,
    DateTime? DtAtualizacao
    )
{
    public static UsersResponse From(Users users)
    {
        return new UsersResponse(
            users.Id,
            users.Nome,
            users.Email ?? string.Empty,
            users.Roles,
            users.Ativo,
            users.DtCadastro,
            users.DtAtualizacao
            );
    }
}
