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
        [StringLength(50)]
        string Perfil,

        bool Ativo
    );
}