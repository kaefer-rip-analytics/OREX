using OrexApp.ManterUsuario.Features.AtualizarUsuarioRequest;
using OrexApp.ManterUsuario.Features.CriarUsuarioRequest;
using OrexApp.ManterUsuario.Features.UsuarioResponse;

namespace OrexApp.ManterUsuario.Features.IUsuarioService
{
    public interface IUsuariosService
    {
        Task<List<UsuariosResponse>> GetAll();
        Task<UsuariosResponse?> GetById(int id);
        Task<UsuariosResponse> CreateAsync(CriarUsuariosRequest request);
        Task<UsuariosResponse?> UpdateAsync(int id, AtualizarUsuariosRequest request);
        Task<bool> DeactivatedAsync(int id);
    }
}