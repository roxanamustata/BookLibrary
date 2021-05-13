using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary
{
    public class BookLibraryDbContext:DbContext
    {

        private readonly string _connectionString = "Data Source=RALU\\SQLEXPRESS;Initial Catalog=BookLibraryDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;" +
            "ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public BookLibraryDbContext() : base()
        {

        }

        public BookLibraryDbContext(DbContextOptions<BookLibraryDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }


        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Author>().HasData(
               new Author
               {
                   Id = 1,
                   Name = "Albert Camus"

               },
               new Author
               {
                   Id = 2,
                   Name = "Jane Austen"

               },
              new Author
              {
                  Id = 3,
                  Name = "Lev Tolstoi"

              });

            modelBuilder.Entity<Book>().HasData(
                 new Book
                 {
                     Id = 1,
                     Title = "The Plague",
                     AuthorId = 1,
                     Publisher = "Teora"
                 },
                 new Book
                 {
                     Id = 2,
                     Title = "The Stranger",
                     AuthorId = 1,
                     Publisher = "Teora"
                 },
                 new Book
                 {
                     Id = 3,
                     Title = "Pride and Prejudice",
                     AuthorId = 2,
                     Publisher = "Univers"
                 },
                 new Book
                 {
                     Id = 4,
                     Title = "Emma",
                     AuthorId = 2,
                     Publisher = "Teora"
                 },

                  new Book
                  {
                      Id = 5,
                      Title = "War and Peace",
                      AuthorId = 3,
                      Publisher = "Univers"
                  }

                );

            

        }




        }
}
