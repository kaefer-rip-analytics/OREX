using Microsoft.EntityFrameworkCore;
using OrexApp.ManterUsuario.Features.Usuario;

namespace OrexApp.Infra.Banco
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.ToTable("Usuarios");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(e => e.Email)
                    .IsUnique();

                entity.Property(e => e.Perfil)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Ativo)
                    .IsRequired()
                    .HasDefaultValue(true);

                entity.Property(e => e.DtCadastro)
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(e => e.DtAtualizacao)
                    .IsRequired(false);
            });
        }
    }
}