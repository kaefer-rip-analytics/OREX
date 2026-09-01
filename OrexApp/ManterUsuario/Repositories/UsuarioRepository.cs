using Microsoft.EntityFrameworkCore;

using OrexApp.Banco;
using OrexApp.ManterUsuario.Models;
using OrexApp.ManterUsuario.Interfaces;

namespace OrexApp.ManterUsuario.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> ObterTodosAsync()
        {
            return await _context.Usuarios
                .OrderBy(Usuario => Usuario.Nome)
                .ToListAsync();
        }

        public async Task<Usuario?> ObterPorIdAsync(int id)
        {
            
            return await _context.Usuarios
            .FirstOrDefaultAsync(usuario => usuario.Id == id);
        }

        public async Task<Usuario> CriarAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task AtualizarAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task InativarAsync(Usuario usuario)
        {            
            usuario.Ativo = false;
            usuario.DtAtualizacao = DateTime.UtcNow;

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }
    }
}