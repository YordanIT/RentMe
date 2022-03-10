using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentMe.Data;
using RentMe.Data.Models;

namespace RentMe.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
               : base(options)
        {
        }

        public DbSet<Property> Properties { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Ad> Advertisements { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}