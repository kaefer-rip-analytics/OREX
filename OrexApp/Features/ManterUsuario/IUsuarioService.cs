using OrexApp.Features.ManterUsuario.DTOs.AtualizarUsuarioRequest;
using OrexApp.Features.ManterUsuario.DTOs.CriarUsuarioRequest;
using OrexApp.Features.ManterUsuario.DTOs.UsuarioResponse;

namespace OrexApp.Features.ManterUsuario.IUsuarioService
{
    public interface IUsuariosService
    {
        Task<List<UsuariosResponse>> GetAll();
        Task<UsuariosResponse?> GetById(string id);
        Task<UsuariosResponse> CreateAsync(CriarUsuariosRequest request);
        Task<UsuariosResponse?> UpdateAsync(string id, AtualizarUsuariosRequest request);
        Task<bool> DeactivatedAsync(string id);
    }
}