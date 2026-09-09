using System.ComponentModel.DataAnnotations;
using OrexApp.Features.MantainUser.Roles;

namespace OrexApp.Features.MantainUser.DTOs.UpdateUserRequest
{
    public record UpdateUsersRequest(
        [Required]
        [StringLength(100)]
        string Nome,

        [Required]
        [EmailAddress]
        string Email,

        [Required]
        [EnumDataType(typeof(UserRoles))]
        UserRoles Roles,

        bool Ativo
    );
}