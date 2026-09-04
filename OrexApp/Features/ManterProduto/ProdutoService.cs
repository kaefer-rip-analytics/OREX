using OrexApp.Features.ManterProduto.DTOs.AtualizarProdutoRequest;
using OrexApp.Features.ManterProduto.DTOs.CriarProdutoRequest;
using OrexApp.Features.ManterProduto.DTOs.ProdutoResponse;
using OrexApp.Features.ManterProduto.Produto;
using OrexApp.Features.ManterProduto.IProdutoRepository;
using OrexApp.Features.ManterProduto.IProdutoService;

namespace OrexApp.Features.ManterProduto.ProdutoService
{
    public class ProdutoService : IProdutosService
    {
        private readonly IProdutosRepository _produtoRepository;

        public ProdutoService(IProdutosRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<List<ProdutosResponse>> GetAll()
        {
            var produtos = await _produtoRepository.GetAll();
            return produtos.Select(ProdutosResponse.From).ToList();
        }

        public async Task<ProdutosResponse?> GetById(int id)
        {
            var produto = await _produtoRepository.GetById(id);

            return produto is null ? null : ProdutosResponse.From(produto);
        }

        public async Task<ProdutosResponse> CreateAsync(CriarProdutosRequest request)
        {
            var produto = new Produtos(request.Descricao, request.Ativo);
            
            await _produtoRepository.CreateAsync(produto);

            return ProdutosResponse.From(produto);
        }

        public async Task<ProdutosResponse?> UpdateAsync(int id, AtualizarProdutosRequest request)
        {
            var produto = await _produtoRepository.GetById(id);
            if (produto == null)
            {
                throw new KeyNotFoundException($"Produto com ID {id} não encontrado.");
            }

            produto.Descricao = request.Descricao;
            produto.Ativo = request.Ativo;
            produto.DtAtualizacao = DateTime.UtcNow;

            await _produtoRepository.UpdateAsync(produto);

            return ProdutosResponse.From(produto);
        }

        public async Task<bool> DeactivatedAsync(int id)
        {
            var produto = await _produtoRepository.GetById(id);
            
            if (produto == null)
            {
                return false;
            }

            await _produtoRepository.DeactivatedAsync(produto);

            return true;
        }
    }
}