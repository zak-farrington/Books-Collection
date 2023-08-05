using BooksCollection.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksCollection.Api.Data
{
    public class BooksCollectionApiContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("BookCollectionApiContext");

            optionsBuilder.UseSqlite(connectionString);
        }


        public BooksCollectionApiContext(DbContextOptions<BooksCollectionApiContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Book { get; set; } = default!;
    }
}
