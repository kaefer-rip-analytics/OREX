using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using OrexApp.Features.MantainUser.User;
using OrexApp.Features.MantainProduct.Product;

namespace OrexApp.Infra.Banco;

public class ApplicationDbContext : IdentityDbContext<Users>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    public DbSet<Users> Users { get; set; }
    public DbSet<Products> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Users>(entity =>
        {
            entity.ToTable("Users");

            entity.HasKey(user => user.Id);

            entity.Property(user => user.Nome)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(user => user.Roles)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(user => user.Email)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasIndex(user => user.Email)
                .IsUnique();

            entity.Property(user => user.Ativo)
                .IsRequired()
                .HasDefaultValue(true);

            entity.Property(user => user.DtCadastro)
                .HasDefaultValueSql("GETUTCDATE()");

            entity.Property(user => user.DtAtualizacao)
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

        modelBuilder.Entity<Products>(entity =>
        {
            entity.ToTable("Products");

            entity.HasKey(product => product.Id);

            entity.Property(product => product.Descricao)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(product => product.Ativo)
                .IsRequired()
                .HasDefaultValue(true);

            entity.Property(product => product.DtCadastro)
                .HasDefaultValueSql("GETUTCDATE()");

            entity.Property(product => product.DtAtualizacao)
                .IsRequired(false);
        });
    }
}
