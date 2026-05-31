using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Serviteca.Shared.Entities;

namespace Serviteca.Backend.Data;

public class DataContext : IdentityDbContext<User>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<DocumentType> DocumentTypes { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Use> Uses { get; set; }
    public DbSet<TypeV> Types { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Insurer> Insurers { get; set; }
    public DbSet<Soat> Soats { get; set; }
    public DbSet<OilBrand> OilBrands { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //evita duplicados de variable (Name ) en la bd
        modelBuilder.Entity<Customer>().HasIndex(x => x.Document).IsUnique();
        modelBuilder.Entity<Brand>().HasIndex(x => x.Name).IsUnique();
        modelBuilder.Entity<DocumentType>().HasIndex(x => x.Name).IsUnique();
        modelBuilder.Entity<TypeV>().HasIndex(x => x.Name).IsUnique();
        modelBuilder.Entity<Use>().HasIndex(x => x.Name).IsUnique();
        modelBuilder.Entity<Vehicle>().HasIndex(x => x.Plate).IsUnique();

        // deshabilita el borrado en cascada,
        DisableCascadingDelete(modelBuilder);
    }

    private void DisableCascadingDelete(ModelBuilder modelBuilder)
    {
        var relationships = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys());
        foreach (var relationship in relationships)
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}