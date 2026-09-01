using Microsoft.AspNetCore.Mvc;

using OrexApp.ManterUsuario.Features.AtualizarUsuarioRequest;
using OrexApp.ManterUsuario.Features.CriarUsuarioRequest;
using OrexApp.ManterUsuario.Features.UsuarioResponse;
using OrexApp.ManterUsuario.Features.IUsuarioService;

namespace OrexApp.ManterUsuario.Features.UsuarioController
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuariosService _usuarioService;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IUsuariosService usuarioService, ILogger<UsuarioController> logger)
        {
            _usuarioService = usuarioService;
            _logger = logger;
        }

        /// <summary>
        /// Recuperar todos os usuários
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<UsuariosResponse>>> GetAll()
        {
            try
            {
                var usuarios = await _usuarioService.GetAll();
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
        public async Task<ActionResult<UsuariosResponse>> GetById(int id)
        {
            try
            {
                var usuario = await _usuarioService.GetById(id);
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
        public async Task<ActionResult<UsuariosResponse>> Create([FromBody] CriarUsuariosRequest dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var usuario = await _usuarioService.CreateAsync(dto);
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
        public async Task<ActionResult<UsuariosResponse>> Update(int id, [FromBody] AtualizarUsuariosRequest dto)
        {
            try
            {
                var usuario = await _usuarioService.UpdateAsync(id, dto);
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
                await _usuarioService.DeactivatedAsync(id);
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