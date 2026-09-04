using OrexApp.Features.ManterUsuario.DTOs.AtualizarUsuarioRequest;
using OrexApp.Features.ManterUsuario.DTOs.CriarUsuarioRequest;
using OrexApp.Features.ManterUsuario.DTOs.UsuarioResponse;
using OrexApp.Features.ManterUsuario.Usuario;
using OrexApp.Features.ManterUsuario.IUsuarioRepository;
using OrexApp.Features.ManterUsuario.IUsuarioService;

namespace OrexApp.Features.ManterUsuario.UsuarioService
{
    public class UsuarioService : IUsuariosService
    {
        private readonly IUsuariosRepository _usuarioRepository;

        public UsuarioService(IUsuariosRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<UsuariosResponse>> GetAll()
        {
            var usuarios = await _usuarioRepository.GetAll();
            return usuarios.Select(UsuariosResponse.From).ToList();
        }

        public async Task<UsuariosResponse?> GetById(int id)
        {
            var usuario = await _usuarioRepository.GetById(id);

            return usuario is null ? null : UsuariosResponse.From(usuario);
        }

        public async Task<UsuariosResponse> CreateAsync(CriarUsuariosRequest request)
        {
            /*
            // Validação de email duplicado
            if (await _repository.ExisteEmailAsync(usuario.Email))
                throw new InvalidOperationException("Email já cadastrado no sistema");

            // Validação de CPF duplicado
            if (await _repository.ExisteCpfAsync(usuario.Cpf))
                throw new InvalidOperationException("CPF já cadastrado no sistema");
            */

            var usuario = new Usuarios(request.Nome, request.Email, request.Perfil, request.Ativo);
            
            await _usuarioRepository.CreateAsync(usuario);

            return UsuariosResponse.From(usuario);
        }

        public async Task<UsuariosResponse?> UpdateAsync(int id, AtualizarUsuariosRequest request)
        {
            var usuario = await _usuarioRepository.GetById(id);
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

            await _usuarioRepository.UpdateAsync(usuario);

            return UsuariosResponse.From(usuario);
        }

        public async Task<bool> DeactivatedAsync(int id)
        {
            var usuario = await _usuarioRepository.GetById(id);
            
            if (usuario == null)
            {
                return false;
            }

            await _usuarioRepository.DeactivatedAsync(usuario);

            return true;
        }
    }
}