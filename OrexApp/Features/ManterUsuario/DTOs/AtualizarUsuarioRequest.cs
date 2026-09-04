using System.ComponentModel.DataAnnotations;

namespace OrexApp.Features.ManterUsuario.DTOs.AtualizarUsuarioRequest
{
    public record AtualizarUsuariosRequest(
        [Required]
        [StringLength(100)]
        string Nome,

        [Required]
        [EmailAddress]
        string Email,

        [Required]
        [StringLength(50)]
        string Perfil,

        bool Ativo
    );
}