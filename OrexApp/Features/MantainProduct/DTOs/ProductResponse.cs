using OrexApp.Features.MantainProduct.Product;

namespace OrexApp.Features.MantainProduct.DTOs.ProductResponse
{
    public record ProductsResponse(
        int Id, 
        string Descricao,
        bool Ativo,
        DateTime? DtCadastro,
        DateTime? DtAtualizacao
        )
    {
        public static ProductsResponse From(Products products)
        {
            return new ProductsResponse(
                products.Id,
                products.Descricao,
                products.Ativo,
                products.DtCadastro,
                products.DtAtualizacao
                );
        }
    }
}