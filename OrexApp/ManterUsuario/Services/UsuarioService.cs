using OrexApp.ManterUsuario.DTOs.Request;
using OrexApp.ManterUsuario.DTOs.Response;
using OrexApp.ManterUsuario.Models;
using OrexApp.ManterUsuario.Interfaces;

namespace OrexApp.ManterUsuario.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<UsuarioResponse>> ObterTodosAsync()
        {
            var usuarios = await _usuarioRepository.ObterTodosAsync();
            return usuarios.Select(UsuarioResponse.From).ToList();
        }

        public async Task<UsuarioResponse?> ObterPorIdAsync(int id)
        {
            var usuario = await _usuarioRepository.ObterPorIdAsync(id);

            return usuario is null ? null : UsuarioResponse.From(usuario);
        }

        public async Task<UsuarioResponse> CriarAsync(CriarUsuarioRequest request)
        {
            /*
            // Validação de email duplicado
            if (await _repository.ExisteEmailAsync(usuario.Email))
                throw new InvalidOperationException("Email já cadastrado no sistema");

            // Validação de CPF duplicado
            if (await _repository.ExisteCpfAsync(usuario.Cpf))
                throw new InvalidOperationException("CPF já cadastrado no sistema");
            */

            var usuario = new Usuario(request.Nome, request.Email, request.Perfil, request.Ativo);
            
            await _usuarioRepository.CriarAsync(usuario);

            return UsuarioResponse.From(usuario);
        }

        public async Task<UsuarioResponse?> AtualizarAsync(int id, AtualizarUsuarioRequest request)
        {
            var usuario = await _usuarioRepository.ObterPorIdAsync(id);
            if (usuario == null)
            {
                throw new KeyNotFoundException($"Usuário com ID {id} não encontrado.");
            }

            /*
            // Validação de email duplicado
            if (await _repository.ExisteEmailAsync(usuario.Email, usuario.Id))
                throw new InvalidOperationException("Email já cadastrado para outro usuário");

            // Validação de CPF duplicado
            if (await _repository.ExisteCpfAsync(usuario.Cpf, usuario.Id))
                throw new InvalidOperationException("CPF já cadastrado para outro usuário");
            */

            usuario.Nome = request.Nome;
            usuario.Email = request.Email;
            usuario.Perfil = request.Perfil;
            usuario.Ativo = request.Ativo;
            usuario.DtAtualizacao = DateTime.UtcNow;

            await _usuarioRepository.AtualizarAsync(usuario);

            return UsuarioResponse.From(usuario);
        }

        public async Task<bool> InativarAsync(int id)
        {
            var usuario = await _usuarioRepository.ObterPorIdAsync(id);
            
            if (usuario == null)
            {
                return false;
            }

            await _usuarioRepository.InativarAsync(usuario);

            return true;
        }
    }
}