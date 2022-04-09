using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RentMe.Infrastructure.Data;

namespace RentMe.Test
{
    //IMPORTANT:
    //Disconnect ApplicationDbContext from SQL local server before testing :
    //Comment method - protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    public class InMemoryDbContext
    {
        private readonly SqliteConnection connection;
        private readonly DbContextOptions<ApplicationDbContext> dbContextOptions;

        public InMemoryDbContext()
        {
            connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection)
                .Options;

            using var context = new ApplicationDbContext(dbContextOptions);

            context.Database.EnsureCreated();
        }

        public ApplicationDbContext CreateContext() => new ApplicationDbContext(dbContextOptions);

        public void Dispose() => connection.Dispose();
    }
}
