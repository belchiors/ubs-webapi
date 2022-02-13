using Microsoft.EntityFrameworkCore;

using BuscaSaude.Models;

namespace BuscaSaude.Data;

public partial class DatabaseContext : DbContext
{
    public DbSet<Unidade>? Unidades { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Unidade>(entity =>
        {
            entity.HasKey(e => e.Cnes)
                .HasName("unidades_pkey");

            entity.ToTable("unidades");

            entity.Property(e => e.Cnes)
                .HasMaxLength(7)
                .HasColumnName("cnes");

            entity.Property(e => e.Bairro).HasColumnName("bairro");

            entity.Property(e => e.Ibge)
                .HasMaxLength(6)
                .HasColumnName("ibge");

            entity.Property(e => e.Latitude).HasColumnName("latitude");

            entity.Property(e => e.Logradouro).HasColumnName("logradouro");

            entity.Property(e => e.Longitude).HasColumnName("longitude");

            entity.Property(e => e.Nome).HasColumnName("nome");

            entity.Property(e => e.Uf).HasColumnName("uf");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}