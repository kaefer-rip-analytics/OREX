using System.ComponentModel.DataAnnotations;
using OrexApp.Features.MantainUser.Roles;

namespace OrexApp.Features.MantainUser.DTOs.CreateUserRequest
{
    public record CreateUsersRequest(
        [Required]
        [StringLength(100)]
        string Nome,

        [Required]
        [EmailAddress]
        string Email,

        [Required]
        [EnumDataType(typeof(UserRoles))]
        UserRoles Roles,

        [Required]
        [MinLength(6)]
        string Password,

        bool Ativo
    );
}