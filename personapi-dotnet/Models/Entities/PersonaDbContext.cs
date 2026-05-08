using Microsoft.EntityFrameworkCore;

namespace personapi_dotnet.Models.Entities;

public class PersonaDbContext : DbContext
{
    public PersonaDbContext(DbContextOptions<PersonaDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Persona> Personas => Set<Persona>();
    public virtual DbSet<Profesion> Profesiones => Set<Profesion>();
    public virtual DbSet<Estudios> Estudios => Set<Estudios>();
    public virtual DbSet<Telefono> Telefonos => Set<Telefono>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estudios>()
            .HasKey(e => new { e.IdProf, e.CcPer });

        modelBuilder.Entity<Estudios>()
            .HasOne(e => e.Profesion)
            .WithMany(p => p.Estudios)
            .HasForeignKey(e => e.IdProf)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Estudios>()
            .HasOne(e => e.Persona)
            .WithMany(p => p.Estudios)
            .HasForeignKey(e => e.CcPer)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Estudios>()
            .HasIndex(e => e.CcPer)
            .HasDatabaseName("estudio_persona_fk");

        modelBuilder.Entity<Telefono>()
            .HasOne(t => t.Persona)
            .WithMany(p => p.Telefonos)
            .HasForeignKey(t => t.Duenio)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Telefono>()
            .HasIndex(t => t.Duenio)
            .HasDatabaseName("telefono_persona_fk");

        modelBuilder.Entity<Persona>()
            .ToTable(tb => tb.HasCheckConstraint("CK_persona_gen", "genero IN ('M','F')"));

        base.OnModelCreating(modelBuilder);
    }
}
