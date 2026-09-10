using OrexApp.Features.MantainUser.User;

namespace OrexApp.Features.MantainUser.DTOs.UserResponse;

public record UsersResponse(
    string Id, 
    string Nome, 
    string Email, 
    bool Ativo,
    DateTime? DtCadastro,
    DateTime? DtAtualizacao,
    IList<string> Roles
    )
{
    public static UsersResponse From(
        Users users,
        IList<string>? roles)
    {
        return new UsersResponse(
            users.Id,
            users.Nome,
            users.Email ?? string.Empty,
            users.Ativo,
            users.DtCadastro,
            users.DtAtualizacao,
            roles ?? []
            );
    }
}
