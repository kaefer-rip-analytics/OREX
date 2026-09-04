using System.ComponentModel.DataAnnotations;

namespace OrexApp.Features.ManterProduto.DTOs.AtualizarProdutoRequest
{
    public record AtualizarProdutosRequest(
        [Required]
        [StringLength(100)]
        string Descricao,

        bool Ativo
    );
}