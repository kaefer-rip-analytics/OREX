using OrexApp.Features.MantainProduct.Product;

namespace OrexApp.Features.MantainProduct.IProductRepository
{
    public interface IProductsRepository
    {
        Task<List<Products>> GetAll();
        Task<Products?> GetById(int id);
        Task<Products> CreateAsync(Products products);
        Task UpdateAsync(Products products);
        Task DeactivatedAsync(Products products);
    }
}