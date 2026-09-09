using OrexApp.Features.MantainProduct.DTOs.UpdateProductRequest;
using OrexApp.Features.MantainProduct.DTOs.CreateProductRequest;
using OrexApp.Features.MantainProduct.DTOs.ProductResponse;

namespace OrexApp.Features.MantainProduct.IProductService
{
    public interface IProductsService
    {
        Task<List<ProductsResponse>> GetAll();
        Task<ProductsResponse?> GetById(int id);
        Task<ProductsResponse> CreateAsync(CreateProductsRequest request);
        Task<ProductsResponse?> UpdateAsync(int id, UpdateProductsRequest request);
        Task<bool> DeactivatedAsync(int id);
    }
}