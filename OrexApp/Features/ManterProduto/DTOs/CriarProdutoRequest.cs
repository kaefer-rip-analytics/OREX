using System.ComponentModel.DataAnnotations;

namespace OrexApp.Features.ManterProduto.DTOs.CriarProdutoRequest
{
    public record CriarProdutosRequest(
        [Required]
        [StringLength(100)]
        string Descricao,

        bool Ativo
    );
}