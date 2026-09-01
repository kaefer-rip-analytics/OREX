using OrexApp.ManterUsuario.Models;

namespace OrexApp.ManterUsuario.DTOs.Response
{
    public record UsuarioResponse(
        int Id, 
        string Nome, 
        string Email, 
        string Perfil, 
        bool Ativo,
        DateTime? DtCadastro,
        DateTime? DtAtualizacao
        )
    {
        public static UsuarioResponse From(Usuario usuario)
        {
            return new UsuarioResponse(
                usuario.Id,
                usuario.Nome,
                usuario.Email,
                usuario.Perfil,
                usuario.Ativo,
                usuario.DtCadastro,
                usuario.DtAtualizacao
                );
        }
    }
}