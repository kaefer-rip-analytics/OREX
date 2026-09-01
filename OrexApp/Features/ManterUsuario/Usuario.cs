namespace OrexApp.ManterUsuario.Features.Usuario
{
    public class Usuarios
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Perfil { get; set; }
        public bool Ativo { get; set; }
        public DateTime DtCadastro { get; set; }
        public DateTime? DtAtualizacao { get; set; }

        public Usuarios()
        {
        }
        public Usuarios (string nome, string email, string perfil, bool ativo)
        {
            Nome = nome;
            Email = email;
            Perfil = perfil;
            Ativo = ativo;
            DtCadastro = DateTime.UtcNow;
        }
    }
}