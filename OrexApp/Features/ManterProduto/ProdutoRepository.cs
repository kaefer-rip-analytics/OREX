using Microsoft.EntityFrameworkCore;

using OrexApp.Infra.Banco;
using OrexApp.Features.ManterProduto.Produto;
using OrexApp.Features.ManterProduto.IProdutoRepository;

namespace OrexApp.Features.ManterProduto.ProdutoRepository
{
    public class ProdutoRepository : IProdutosRepository
    {
        private readonly ApplicationDbContext _context;

        public ProdutoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Produtos>> GetAll()
        {
            return await _context.Produtos
                .OrderBy(Produto => Produto.Descricao)
                .ToListAsync();
        }

        public async Task<Produtos?> GetById(int id)
        {
            
            return await _context.Produtos
            .FirstOrDefaultAsync(produto => produto.Id == id);
        }

        public async Task<Produtos> CreateAsync(Produtos produtos)
        {
            _context.Produtos.Add(produtos);
            await _context.SaveChangesAsync();
            return produtos;
        }

        public async Task UpdateAsync(Produtos produtos)
        {
            _context.Produtos.Update(produtos);
            await _context.SaveChangesAsync();
        }

        public async Task DeactivatedAsync(Produtos produtos)
        {            
            produtos.Ativo = false;
            produtos.DtAtualizacao = DateTime.UtcNow;

            _context.Produtos.Update(produtos);
            await _context.SaveChangesAsync();
        }
    }
}