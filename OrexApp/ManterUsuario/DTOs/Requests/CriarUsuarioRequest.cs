using System.ComponentModel.DataAnnotations;

namespace OrexApp.ManterUsuario.DTOs.Request
{
    public record CriarUsuarioRequest(
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