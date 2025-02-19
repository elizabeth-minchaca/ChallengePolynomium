using ChallengePolynomius.Models;
using Microsoft.EntityFrameworkCore;

namespace ChallengePolynomius.Configurations
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }
        //Define las tablas de la base de datos.
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            DbInitializer.Seed(modelBuilder);
        }

    }
}
