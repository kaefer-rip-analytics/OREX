using Microsoft.EntityFrameworkCore;
using OrexApp.Features.ManterUsuario.Usuario;
using OrexApp.Features.ManterProduto.Produto;

namespace OrexApp.Infra.Banco
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Produtos> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.ToTable("Usuarios");

                entity.HasKey(u => u.Id);

                entity.Property(u => u.Nome)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(u => u.Email)
                    .IsUnique();

                entity.Property(u => u.Perfil)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.Ativo)
                    .IsRequired()
                    .HasDefaultValue(true);

                entity.Property(u => u.DtCadastro)
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(e => e.DtAtualizacao)
                    .IsRequired(false);
            });

            modelBuilder.Entity<Produtos>(entity =>
            {
                entity.ToTable("Produtos");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.Descricao)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.Ativo)
                    .IsRequired()
                    .HasDefaultValue(true);

                entity.Property(p => p.DtCadastro)
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(p => p.DtAtualizacao)
                    .IsRequired(false);
            });
        }
    }
}