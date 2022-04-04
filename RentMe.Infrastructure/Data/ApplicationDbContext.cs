using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentMe.Infrastructure.Data.Identity;
using RentMe.Infrastructure.Data.Models;

namespace RentMe.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
               : base(options)
        {
        }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<Type>();

            modelBuilder.Entity<Tenant>()
                        .HasIndex(t => t.Email)
                        .IsUnique();
        }

    }
}