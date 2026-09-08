using OrexApp.Features.ManterUsuario.Usuario;

namespace OrexApp.Features.ManterUsuario.DTOs.UsuarioResponse;

public record UsuariosResponse(
    string Id, 
    string Nome, 
    string Email, 
    UsuarioPerfil Perfil, 
    bool Ativo,
    DateTime? DtCadastro,
    DateTime? DtAtualizacao
    )
{
    public static UsuariosResponse From(Usuarios usuarios)
    {
        return new UsuariosResponse(
            usuarios.Id,
            usuarios.Nome,
            usuarios.Email ?? string.Empty,
            usuarios.Perfil,
            usuarios.Ativo,
            usuarios.DtCadastro,
            usuarios.DtAtualizacao
            );
    }
}
