using Microsoft.EntityFrameworkCore;

using OrexApp.Infra.Banco;
using OrexApp.Features.ManterUsuario.Usuario;
using OrexApp.Features.ManterUsuario.IUsuarioRepository;

namespace OrexApp.Features.ManterUsuario.UsuarioRepository
{
    public class UsuarioRepository : IUsuariosRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Usuarios>> GetAll()
        {
            return await _context.Usuarios
                .OrderBy(Usuario => Usuario.Nome)
                .ToListAsync();
        }

        public async Task<Usuarios?> GetById(int id)
        {
            
            return await _context.Usuarios
            .FirstOrDefaultAsync(usuario => usuario.Id == id);
        }

        public async Task<Usuarios> CreateAsync(Usuarios usuarios)
        {
            _context.Usuarios.Add(usuarios);
            await _context.SaveChangesAsync();
            return usuarios;
        }

        public async Task UpdateAsync(Usuarios usuarios)
        {
            _context.Usuarios.Update(usuarios);
            await _context.SaveChangesAsync();
        }

        public async Task DeactivatedAsync(Usuarios usuarios)
        {            
            usuarios.Ativo = false;
            usuarios.DtAtualizacao = DateTime.UtcNow;

            _context.Usuarios.Update(usuarios);
            await _context.SaveChangesAsync();
        }
    }
}