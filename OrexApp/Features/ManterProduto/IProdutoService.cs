using OrexApp.Features.ManterProduto.DTOs.AtualizarProdutoRequest;
using OrexApp.Features.ManterProduto.DTOs.CriarProdutoRequest;
using OrexApp.Features.ManterProduto.DTOs.ProdutoResponse;

namespace OrexApp.Features.ManterProduto.IProdutoService
{
    public interface IProdutosService
    {
        Task<List<ProdutosResponse>> GetAll();
        Task<ProdutosResponse?> GetById(int id);
        Task<ProdutosResponse> CreateAsync(CriarProdutosRequest request);
        Task<ProdutosResponse?> UpdateAsync(int id, AtualizarProdutosRequest request);
        Task<bool> DeactivatedAsync(int id);
    }
}