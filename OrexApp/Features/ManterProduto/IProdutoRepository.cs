using OrexApp.Features.ManterProduto.Produto;

namespace OrexApp.Features.ManterProduto.IProdutoRepository
{
    public interface IProdutosRepository
    {
        Task<List<Produtos>> GetAll();
        Task<Produtos?> GetById(int id);
        Task<Produtos> CreateAsync(Produtos produtos);
        Task UpdateAsync(Produtos produtos);
        Task DeactivatedAsync(Produtos produtos);
    }
}