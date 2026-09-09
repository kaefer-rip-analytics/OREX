using Microsoft.AspNetCore.Identity;
using OrexApp.Features.MantainUser.Roles;

namespace OrexApp.Features.MantainUser.User
{
    public class Users : IdentityUser
    {
        public string Nome { get; set; }
        public UserRoles Roles { get; set; }
        public bool Ativo { get; set; }
        public DateTime DtCadastro { get; set; }
        public DateTime? DtAtualizacao { get; set; }

        public Users()
        {
        }
        public Users (string nome, bool ativo)
        {
            Nome = nome;
            Ativo = ativo;
            DtCadastro = DateTime.UtcNow;
        }
    }
}