using OrexApp.Features.ManterProduto.Produto;

namespace OrexApp.Features.ManterProduto.DTOs.ProdutoResponse
{
    public record ProdutosResponse(
        int Id, 
        string Descricao,
        bool Ativo,
        DateTime? DtCadastro,
        DateTime? DtAtualizacao
        )
    {
        public static ProdutosResponse From(Produtos produtos)
        {
            return new ProdutosResponse(
                produtos.Id,
                produtos.Descricao,
                produtos.Ativo,
                produtos.DtCadastro,
                produtos.DtAtualizacao
                );
        }
    }
}