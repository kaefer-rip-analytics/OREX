using OrexApp.ManterUsuario.Features.Usuario;

namespace OrexApp.ManterUsuario.Features.IUsuarioRepository
{
    public interface IUsuariosRepository
    {
        Task<List<Usuarios>> GetAll();
        Task<Usuarios?> GetById(int id);
        Task<Usuarios> CreateAsync(Usuarios usuarios);
        Task UpdateAsync(Usuarios usuarios);
        Task DeactivatedAsync(Usuarios usuarios);
    }
}