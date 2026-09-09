using Microsoft.EntityFrameworkCore;

using OrexApp.Infra.Banco;
using OrexApp.Features.MantainProduct.Product;
using OrexApp.Features.MantainProduct.IProductRepository;

namespace OrexApp.Features.MantainProduct.ProductRepository
{
    public class ProductRepository : IProductsRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Products>> GetAll()
        {
            return await _context.Products
                .OrderBy(Product => Product.Descricao)
                .ToListAsync();
        }

        public async Task<Products?> GetById(int id)
        {
            
            return await _context.Products
            .FirstOrDefaultAsync(product => product.Id == id);
        }

        public async Task<Products> CreateAsync(Products products)
        {
            _context.Products.Add(products);
            await _context.SaveChangesAsync();
            return products;
        }

        public async Task UpdateAsync(Products products)
        {
            _context.Products.Update(products);
            await _context.SaveChangesAsync();
        }

        public async Task DeactivatedAsync(Products products)
        {            
            products.Ativo = false;
            products.DtAtualizacao = DateTime.UtcNow;

            _context.Products.Update(products);
            await _context.SaveChangesAsync();
        }
    }
}