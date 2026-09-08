using Microsoft.AspNetCore.Identity;

namespace OrexApp.Features.ManterUsuario.Usuario
{
    public class Usuarios : IdentityUser
    {
        public string Nome { get; set; }
        public UsuarioPerfil? Perfil { get; set; }
        public bool Ativo { get; set; }
        public DateTime DtCadastro { get; set; }
        public DateTime? DtAtualizacao { get; set; }

        public Usuarios()
        {
        }
        public Usuarios (string nome, bool ativo)
        {
            Nome = nome;
            Ativo = ativo;
            DtCadastro = DateTime.UtcNow;
        }
    }
}