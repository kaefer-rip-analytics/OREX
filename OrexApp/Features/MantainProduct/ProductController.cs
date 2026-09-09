using Microsoft.AspNetCore.Mvc;

using OrexApp.Features.MantainProduct.DTOs.UpdateProductRequest;
using OrexApp.Features.MantainProduct.DTOs.CreateProductRequest;
using OrexApp.Features.MantainProduct.DTOs.ProductResponse;
using OrexApp.Features.MantainProduct.IProductService;

namespace OrexApp.Features.MantainProduct.ProductController
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductsService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductsService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        /// <summary>
        /// Recuperar todos os produtos
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<ProductsResponse>>> GetAll()
        {
            try
            {
                var products = await _productService.GetAll();
                return Ok(products);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar produtos");
                return StatusCode(500, "Erro ao buscar produtos");
            }
        }

        /// <summary>
        /// Recuperar produtos por Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductsResponse>> GetById(int id)
        {
            try
            {
                var product = await _productService.GetById(id);
                return Ok(product);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar produtos");
                return StatusCode(500, "Erro ao buscar produtos");
            }
        }

        /// <summary>
        /// Criar produtos com objeto
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ProductsResponse>> Create([FromBody] CreateProductsRequest dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var product = await _productService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao criar produtos");
                return StatusCode(500, "Erro ao criar produtos");
            }
        }

        /// <summary>
        /// Atualizar produtos com objeto
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductsResponse>> Update(int id, [FromBody] UpdateProductsRequest dto)
        {
            try
            {
                var product = await _productService.UpdateAsync(id, dto);
                return Ok(product);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao atualizar produtos");
                return StatusCode(500, "Erro ao atualizar produtos");
            }
        }

        /// <summary>
        /// Exclui um produtos por Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Inativar(int id)
        {
            try
            {
                await _productService.DeactivatedAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao Inativar produtos");
                return StatusCode(500, "Erro ao Inativar produtos");
            }
        }
    }
}