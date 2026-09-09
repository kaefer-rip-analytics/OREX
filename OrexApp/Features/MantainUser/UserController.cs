using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using OrexApp.Features.MantainUser.DTOs.UpdateUserRequest;
using OrexApp.Features.MantainUser.DTOs.CreateUserRequest;
using OrexApp.Features.MantainUser.DTOs.UserResponse;
using OrexApp.Features.MantainUser.IUserService;

namespace OrexApp.Features.MantainUser.UserController
{
    [Authorize]
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUsersService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUsersService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Recuperar todos os usuários
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<UsersResponse>>> GetAll()
        {
            try
            {
                var users = await _userService.GetAll();
                return Ok(users);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar usuários");
                return StatusCode(500, "Erro ao buscar usuários");
            }
        }

        /// <summary>
        /// Somente ADM pode visualizar usuário por Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<UsersResponse>> GetById(string id)
        {
            try
            {
                var user = await _userService.GetById(id);
                return Ok(user);
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
        /// Somente ADM pode criar usuário com objeto
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<UsersResponse>> Create([FromBody] CreateUsersRequest dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = await _userService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
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
        /// Somente ADM pode atualizar usuário com objeto
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<UsersResponse>> Update(string id, [FromBody] UpdateUsersRequest dto)
        {
            try
            {
                var user = await _userService.UpdateAsync(id, dto);
                return Ok(user);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao atualizar usuário");
                return StatusCode(500, "Erro ao atualizar usuário");
            }
        }

        /// <summary>
        /// Somente ADM pode visualizar exclui um usuário por Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Inativar(string id)
        {
            try
            {
                await _userService.DeactivatedAsync(id);
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