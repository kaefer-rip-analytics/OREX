using Microsoft.AspNetCore.Mvc;

using OrexApp.ManterUsuario.DTOs.Request;
using OrexApp.ManterUsuario.DTOs.Response;
using OrexApp.ManterUsuario.Interfaces;

namespace OrexApp.ManterUsuario.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IUsuarioService usuarioService, ILogger<UsuarioController> logger)
        {
            _usuarioService = usuarioService;
            _logger = logger;
        }

        /// <summary>
        /// Recuperar todos os usuários
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<UsuarioResponse>>> GetAll()
        {
            try
            {
                var usuarios = await _usuarioService.ObterTodosAsync();
                return Ok(usuarios);
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
        public async Task<ActionResult<UsuarioResponse>> GetById(int id)
        {
            try
            {
                var usuario = await _usuarioService.ObterPorIdAsync(id);
                return Ok(usuario);
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
        public async Task<ActionResult<UsuarioResponse>> Create([FromBody] CriarUsuarioRequest dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var usuario = await _usuarioService.CriarAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
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
        public async Task<ActionResult<UsuarioResponse>> Update(int id, [FromBody] AtualizarUsuarioRequest dto)
        {
            try
            {
                var usuario = await _usuarioService.AtualizarAsync(id, dto);
                return Ok(usuario);
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
                await _usuarioService.InativarAsync(id);
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