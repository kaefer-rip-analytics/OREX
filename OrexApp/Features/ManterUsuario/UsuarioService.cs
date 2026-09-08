using OrexApp.Features.ManterUsuario.DTOs.AtualizarUsuarioRequest;
using OrexApp.Features.ManterUsuario.DTOs.CriarUsuarioRequest;
using OrexApp.Features.ManterUsuario.DTOs.UsuarioResponse;
using OrexApp.Features.ManterUsuario.Usuario;
using OrexApp.Features.ManterUsuario.IUsuarioService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace OrexApp.Features.ManterUsuario.UsuarioService
{
    public class UsuarioService : IUsuariosService
    {
        private readonly UserManager<Usuarios> _userManager;

        public UsuarioService(UserManager<Usuarios> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<UsuariosResponse>> GetAll()
        {
            var usuarios = await _userManager.Users
                .Select(usuario => new UsuariosResponse(
                    usuario.Id,
                    usuario.Nome,
                    usuario.Email,
                    usuario.Perfil,
                    usuario.Ativo,
                    usuario.DtCadastro,
                    usuario.DtAtualizacao)).ToListAsync();

            return usuarios;
        }

        public async Task<UsuariosResponse?> GetById(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);

            return UsuariosResponse.From(usuario);
        }

        public async Task<UsuariosResponse> CreateAsync(CriarUsuariosRequest request)
        {
            var usuario = new Usuarios
            {
                UserName = request.Nome,
                Nome = request.Nome,
                Email = request.Email,
                Perfil = request.Perfil,
                Ativo = request.Ativo,
                DtCadastro = DateTime.UtcNow
            };
            
            await _userManager.CreateAsync(usuario, request.Password);

            return UsuariosResponse.From(usuario);
        }

        public async Task<UsuariosResponse?> UpdateAsync(string id, AtualizarUsuariosRequest request)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null)
            {
                throw new KeyNotFoundException($"Usuário com ID {id} não encontrado.");
            }

            usuario.Nome = request.Nome;
            usuario.Ativo = request.Ativo;
            usuario.DtAtualizacao = DateTime.UtcNow;

            await _userManager.UpdateAsync(usuario);

            return UsuariosResponse.From(usuario);
        }

        public async Task<bool> DeactivatedAsync(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            
            if (usuario == null)
            {
                return false;
            }

            usuario.Ativo = false;
            usuario.DtAtualizacao = DateTime.UtcNow;
            
            await _userManager.UpdateAsync(usuario);

            return true;
        }
    }
}