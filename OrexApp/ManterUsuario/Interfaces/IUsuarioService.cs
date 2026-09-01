using OrexApp.ManterUsuario.DTOs.Request;
using OrexApp.ManterUsuario.DTOs.Response;

namespace OrexApp.ManterUsuario.Interfaces
{
    public interface IUsuarioService
    {
        Task<List<UsuarioResponse>> ObterTodosAsync();
        Task<UsuarioResponse?> ObterPorIdAsync(int id);
        Task<UsuarioResponse> CriarAsync(CriarUsuarioRequest request);
        Task<UsuarioResponse?> AtualizarAsync(int id, AtualizarUsuarioRequest request);
        Task<bool> InativarAsync(int id);
    }
}