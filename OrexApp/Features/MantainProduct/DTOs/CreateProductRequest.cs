using System.ComponentModel.DataAnnotations;

namespace OrexApp.Features.MantainProduct.DTOs.CreateProductRequest
{
    public record CreateProductsRequest(
        [Required]
        [StringLength(100)]
        string Descricao,

        bool Ativo
    );
}