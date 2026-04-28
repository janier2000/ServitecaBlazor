using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Serviteca.Shared.Entities;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace Serviteca.Backend.Data
{
    public class DataContext : IdentityDbContext<User>
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<CartType> CarTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //evita duplicados de variable (Name ) en la bd 
            modelBuilder.Entity<Customer>().HasIndex(x => x.Document).IsUnique();
            modelBuilder.Entity<Brand>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<DocumentType>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<VehicleType>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<CartType>().HasIndex(x => x.Name).IsUnique();
        }
    }
}