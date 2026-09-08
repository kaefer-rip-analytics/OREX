using System.ComponentModel.DataAnnotations;

namespace OrexApp.Features.ManterUsuario.DTOs.CriarUsuarioRequest
{
    public record CriarUsuariosRequest(
        [Required]
        [StringLength(100)]
        string Nome,

        [Required]
        [EmailAddress]
        string Email,

        [Required]
        UsuarioPerfil Perfil,

        [Required]
        [MinLength(6)]
        string Password,

        bool Ativo
    );
}