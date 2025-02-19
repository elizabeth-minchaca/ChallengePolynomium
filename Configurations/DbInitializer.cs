using Microsoft.EntityFrameworkCore;
using ChallengePolynomius.Models;

namespace ChallengePolynomius.Configurations
{
    public static class DbInitializer
    {
        public static void Seed(LibraryContext context)
        {
            if (!context.Authors.Any())
            {
                // Crear autores
                var authors = new List<Author>
                {
                    new Author { Name = "J.K. Rowling" },
                    new Author { Name = "J.R.R. Tolkien" },
                    new Author { Name = "George Orwell" },
                    new Author { Name = "Isaac Asimov" },
                    new Author { Name = "Agatha Christie" },
                    new Author { Name = "Stephen King" }
                };

                context.Authors.AddRange(authors);
                context.SaveChanges();
            }

            if (!context.Categories.Any())
            {
                // Crear categorías
                var categories = new List<Category>
                {
                    new Category { Name = "Fantasía" },
                    new Category { Name = "Ciencia Ficción" },
                    new Category { Name = "Novela Gráfica" },
                    new Category { Name = "Suspenso" },
                    new Category { Name = "Misterio" },
                    new Category { Name = "Terror" }
                };

                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            if (!context.Books.Any())
            {
                // Crear libros
                var books = new List<Book>
                {
                    // J.K. Rowling
                    new Book { Title = "Harry Potter y la Piedra Filosofal", AuthorId = 1, CategoryId = 1 },
                    new Book { Title = "Harry Potter y la Cámara Secreta", AuthorId = 1, CategoryId = 1 },
                    new Book { Title = "Harry Potter y el Prisionero de Azkaban", AuthorId = 1, CategoryId = 1 },

                    // J.R.R. Tolkien
                    new Book { Title = "El Señor de los Anillos: La Comunidad del Anillo", AuthorId = 2, CategoryId = 1 },
                    new Book { Title = "El Señor de los Anillos: Las Dos Torres", AuthorId = 2, CategoryId = 1 },
                    new Book { Title = "El Señor de los Anillos: El Retorno del Rey", AuthorId = 2, CategoryId = 1 },

                    // George Orwell
                    new Book { Title = "1984", AuthorId = 3, CategoryId = 2 },
                    new Book { Title = "Rebelión en la Granja", AuthorId = 3, CategoryId = 2 },

                    // Isaac Asimov
                    new Book { Title = "Fundación", AuthorId = 4, CategoryId = 2 },
                    new Book { Title = "Yo, Robot", AuthorId = 4, CategoryId = 2 },

                    // Agatha Christie
                    new Book { Title = "Asesinato en el Orient Express", AuthorId = 5, CategoryId = 5 },
                    new Book { Title = "Diez Negritos", AuthorId = 5, CategoryId = 5 },

                    // Stephen King
                    new Book { Title = "It", AuthorId = 6, CategoryId = 6 },
                    new Book { Title = "El Resplandor", AuthorId = 6, CategoryId = 6 },
                    new Book { Title = "Cementerio de Animales", AuthorId = 6, CategoryId = 6 }
                };

                context.Books.AddRange(books);
                context.SaveChanges();
            }
        }
    }
}
