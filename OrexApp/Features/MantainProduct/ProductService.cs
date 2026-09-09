using OrexApp.Features.MantainProduct.DTOs.UpdateProductRequest;
using OrexApp.Features.MantainProduct.DTOs.CreateProductRequest;
using OrexApp.Features.MantainProduct.DTOs.ProductResponse;
using OrexApp.Features.MantainProduct.Product;
using OrexApp.Features.MantainProduct.IProductRepository;
using OrexApp.Features.MantainProduct.IProductService;

namespace OrexApp.Features.MantainProduct.ProductService
{
    public class ProductService : IProductsService
    {
        private readonly IProductsRepository _productRepository;

        public ProductService(IProductsRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductsResponse>> GetAll()
        {
            var products = await _productRepository.GetAll();
            return products.Select(ProductsResponse.From).ToList();
        }

        public async Task<ProductsResponse?> GetById(int id)
        {
            var product = await _productRepository.GetById(id);

            return product is null ? null : ProductsResponse.From(product);
        }

        public async Task<ProductsResponse> CreateAsync(CreateProductsRequest request)
        {
            var product = new Products(request.Descricao, request.Ativo);
            
            await _productRepository.CreateAsync(product);

            return ProductsResponse.From(product);
        }

        public async Task<ProductsResponse?> UpdateAsync(int id, UpdateProductsRequest request)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Produto com ID {id} não encontrado.");
            }

            product.Descricao = request.Descricao;
            product.Ativo = request.Ativo;
            product.DtAtualizacao = DateTime.UtcNow;

            await _productRepository.UpdateAsync(product);

            return ProductsResponse.From(product);
        }

        public async Task<bool> DeactivatedAsync(int id)
        {
            var product = await _productRepository.GetById(id);
            
            if (product == null)
            {
                return false;
            }

            await _productRepository.DeactivatedAsync(product);

            return true;
        }
    }
}