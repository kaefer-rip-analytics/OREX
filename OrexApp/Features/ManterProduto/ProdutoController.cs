using Microsoft.AspNetCore.Mvc;

using OrexApp.Features.ManterProduto.DTOs.AtualizarProdutoRequest;
using OrexApp.Features.ManterProduto.DTOs.CriarProdutoRequest;
using OrexApp.Features.ManterProduto.DTOs.ProdutoResponse;
using OrexApp.Features.ManterProduto.IProdutoService;

namespace OrexApp.Features.ManterProduto.ProdutoController
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutosService _produtoService;
        private readonly ILogger<ProdutoController> _logger;

        public ProdutoController(IProdutosService produtoService, ILogger<ProdutoController> logger)
        {
            _produtoService = produtoService;
            _logger = logger;
        }

        /// <summary>
        /// Recuperar todos os produtos
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<ProdutosResponse>>> GetAll()
        {
            try
            {
                var produtos = await _produtoService.GetAll();
                return Ok(produtos);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar usuários");
                return StatusCode(500, "Erro ao buscar usuários");
            }
        }

        /// <summary>
        /// Recuperar usuário por Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutosResponse>> GetById(int id)
        {
            try
            {
                var produto = await _produtoService.GetById(id);
                return Ok(produto);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar usuário");
                return StatusCode(500, "Erro ao buscar usuário");
            }
        }

        /// <summary>
        /// Criar usuário com objeto
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ProdutosResponse>> Create([FromBody] CriarProdutosRequest dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var produto = await _produtoService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao criar usuário");
                return StatusCode(500, "Erro ao criar usuário");
            }
        }

        /// <summary>
        /// Atualizar usuário com objeto
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ProdutosResponse>> Update(int id, [FromBody] AtualizarProdutosRequest dto)
        {
            try
            {
                var produto = await _produtoService.UpdateAsync(id, dto);
                return Ok(produto);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao atualizar usuário");
                return StatusCode(500, "Erro ao atualizar usuário");
            }
        }

        /// <summary>
        /// Exclui um usuário por Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Inativar(int id)
        {
            try
            {
                await _produtoService.DeactivatedAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao Inativar usuário");
                return StatusCode(500, "Erro ao Inativar usuário");
            }
        }
    }
}