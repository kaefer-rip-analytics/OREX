using System.ComponentModel.DataAnnotations;

namespace OrexApp.Features.MantainProduct.DTOs.UpdateProductRequest
{
    public record UpdateProductsRequest(
        [Required]
        [StringLength(100)]
        string Descricao,

        bool Ativo
    );
}