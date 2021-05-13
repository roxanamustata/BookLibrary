using BookLibrary.Models;
using System;
using System.Linq;

namespace BookLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            DisplayNumberOfBooksForEachPublisher();
            DisplayPublishersWithMoreThanNumberOfBooks(2);
            DisplayAuthorsWithBooksAndPublishers();
            DisplayAuthorsWithAverageBookPrice();
            DisplayAuthorsWithAllBookPricesGreaterThan(40);
            DisplayAuthorsWithAtLeastABookPriceLowerThan(80);
            DisplayBooksOnCategories();



        }
        public static void DisplayNumberOfBooksForEachPublisher()
        {
            using (var context = new BookLibraryDbContext())
            {

                var groupByPublisher = context.Books
                                       .GroupBy(b => b.Publisher)
                                       .Select(b => new
                                       {
                                           Publisher = b.Key,
                                           NumberOfBooks = b.Count()
                                       });

                foreach (var item in groupByPublisher)
                {
                    Console.WriteLine($"{item.Publisher}: {item.NumberOfBooks} books");

                }
            }
        }
        public static void DisplayPublishersWithMoreThanNumberOfBooks(int numberOfBooks)
        {
            using (var context = new BookLibraryDbContext())
            {
                var groupByPublisher = context.Books
                                       .GroupBy(b => b.Publisher)
                                       .Select(b => new
                                       {
                                           Publisher = b.Key,
                                           NumberOfBooks = b.Count()
                                       })
                                       .Where(p=>p.NumberOfBooks>numberOfBooks);

                foreach (var item in groupByPublisher)
                {
                    Console.WriteLine($"{item.Publisher}: {item.NumberOfBooks} books");

                }
            }
        }

        public static void DisplayAuthorsWithBooksAndPublishers()
        {
            using (var context = new BookLibraryDbContext())
            {
                var authorWithBookAndPublisher = context.Authors
                                       .SelectMany(a => a.Books, (a, b) => new
                                       {
                                           Author = a.Name,
                                           Book = b.Title,
                                           Publisher = b.Publisher
                                       }) ;

                foreach (var item in authorWithBookAndPublisher)
                {
                    Console.WriteLine($"{item.Author}, {item.Book}, {item.Publisher}");

                }
            }
        }

        public static void DisplayAuthorsWithAverageBookPrice()
        {
            using (var context = new BookLibraryDbContext())
            {
                var authorsWithAverageBookPrice = context.Authors
                                       .Select(a=>new
                                       {
                                           Author = a.Name,
                                           AveragePrice = a.Books.Average(b=>b.Price)
                                       });

                foreach (var item in authorsWithAverageBookPrice)
                {
                    Console.WriteLine($"{item.Author}, {item.AveragePrice} EUR");

                }
            }
        }

        public static void DisplayAuthorsWithAllBookPricesGreaterThan(double bookPrice)
        {
            using (var context = new BookLibraryDbContext())
            {
                var authorsWithBookPriceGreaterThan = context.Authors
                                       .Where(a => a.Books.All(b => b.Price > bookPrice));

                foreach (var item in authorsWithBookPriceGreaterThan)
                {
                    Console.WriteLine($"For {item.Name} all books have prices greater than: {bookPrice}");

                }
            }
        }

        public static void DisplayAuthorsWithAtLeastABookPriceLowerThan(double bookPrice)
        {
            using (var context = new BookLibraryDbContext())
            {
                var authorsWithBookPriceGreaterThan = context.Authors
                                       .Where(a => a.Books.Any(b => b.Price > bookPrice));

                foreach (var item in authorsWithBookPriceGreaterThan)
                {
                    Console.WriteLine($"For {item.Name} there is at least a book with price lower than: {bookPrice}");

                }
            }
        }


        public static void DisplayBooksOnCategories()
        {
            using (var context = new BookLibraryDbContext())
            {
                var categorizeBooks = context
                                    .Books
                                    .Select(b => new 
                                     {
                                         Title = b.Title,
                                         Category = b.Price >=80 ?
                                     "Expensive" : "Affordable"
                                     });



                foreach (var item in categorizeBooks)
                {
                    Console.WriteLine($"Book {item.Title}: {item.Category} category");

                }
            }
        }


    }
}
