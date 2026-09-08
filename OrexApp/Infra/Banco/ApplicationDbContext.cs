using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using OrexApp.Features.ManterUsuario.Usuario;
using OrexApp.Features.ManterProduto.Produto;

namespace OrexApp.Infra.Banco;

public class ApplicationDbContext : IdentityDbContext<Usuarios>
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

            entity.HasKey(usuario => usuario.Id);

            entity.Property(usuario => usuario.Nome)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(usuario => usuario.Perfil)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(usuario => usuario.Email)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasIndex(usuario => usuario.Email)
                .IsUnique();

            entity.Property(usuario => usuario.Ativo)
                .IsRequired()
                .HasDefaultValue(true);

            entity.Property(usuario => usuario.DtCadastro)
                .HasDefaultValueSql("GETUTCDATE()");

            entity.Property(usuario => usuario.DtAtualizacao)
                .IsRequired(false);
        });

        modelBuilder.Entity<IdentityRole>()
            .ToTable("Roles");

        modelBuilder.Entity<IdentityUserRole<string>>()
            .ToTable("UserRoles");

        modelBuilder.Entity<IdentityUserClaim<string>>()
            .ToTable("UserClaims");

        modelBuilder.Entity<IdentityUserLogin<string>>()
            .ToTable("UserLogins");

        modelBuilder.Entity<IdentityRoleClaim<string>>()
            .ToTable("RoleClaims");

        modelBuilder.Entity<IdentityUserToken<string>>()
            .ToTable("UserTokens");

        modelBuilder.Entity<Produtos>(entity =>
        {
            entity.ToTable("Produtos");

            entity.HasKey(produto => produto.Id);

            entity.Property(produto => produto.Descricao)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(produto => produto.Ativo)
                .IsRequired()
                .HasDefaultValue(true);

            entity.Property(produto => produto.DtCadastro)
                .HasDefaultValueSql("GETUTCDATE()");

            entity.Property(produto => produto.DtAtualizacao)
                .IsRequired(false);
        });
    }
}
