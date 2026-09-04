using OrexApp.Features.ManterUsuario.Usuario;

namespace OrexApp.Features.ManterUsuario.DTOs.UsuarioResponse
{
    public record UsuariosResponse(
        int Id, 
        string Nome, 
        string Email, 
        string Perfil, 
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
                usuarios.Email,
                usuarios.Perfil,
                usuarios.Ativo,
                usuarios.DtCadastro,
                usuarios.DtAtualizacao
                );
        }
    }
}