using OrexApp.ManterUsuario.Models;

namespace OrexApp.ManterUsuario.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> ObterTodosAsync();
        Task<Usuario?> ObterPorIdAsync(int id);
        Task<Usuario> CriarAsync(Usuario usuario);
        Task AtualizarAsync(Usuario usuario);
        Task InativarAsync(Usuario usuario);
    }
}