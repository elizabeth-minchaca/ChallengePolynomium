using Microsoft.EntityFrameworkCore;
using ChallengePolynomius.Models;

namespace ChallengePolynomius.Configurations
{
    public static class DbInitializer
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                    new Author { Id = 1, Name = "J.K. Rowling" },
                    new Author { Id = 2, Name = "J.R.R. Tolkien" },
                    new Author {Id = 3, Name = "George Orwell" },
                    new Author {Id = 4, Name = "Isaac Asimov" },
                    new Author {Id = 5, Name = "Agatha Christie" },
                    new Author {Id = 6, Name = "Stephen King" }
                );



            modelBuilder.Entity<Category>().HasData(
                    new Category {Id = 1, Name = "Fantasía" },
                    new Category { Id = 2, Name = "Ciencia Ficción" },
                    new Category { Id = 3, Name = "Novela Gráfica" },
                    new Category { Id = 4, Name = "Suspenso" },
                    new Category { Id = 5, Name = "Misterio" },
                    new Category { Id = 6, Name = "Terror" }
                );

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Harry Potter y la Piedra Filosofal", AuthorId = 1, CategoryId = 1 },
                    new Book { Id = 2, Title = "Harry Potter y la Cámara Secreta", AuthorId = 1, CategoryId = 1 },
                    new Book { Id = 3, Title = "Harry Potter y el Prisionero de Azkaban", AuthorId = 1, CategoryId = 1 },

                    // J.R.R. Tolkien
                    new Book { Id = 4, Title = "El Señor de los Anillos: La Comunidad del Anillo", AuthorId = 2, CategoryId = 1 },
                    new Book { Id = 5, Title = "El Señor de los Anillos: Las Dos Torres", AuthorId = 2, CategoryId = 1 },
                    new Book { Id = 6, Title = "El Señor de los Anillos: El Retorno del Rey", AuthorId = 2, CategoryId = 1 },

                    // George Orwell
                    new Book { Id = 7, Title = "1984", AuthorId = 3, CategoryId = 2 },
                    new Book { Id = 8, Title = "Rebelión en la Granja", AuthorId = 3, CategoryId = 2 },

                    // Isaac Asimov
                    new Book { Id = 9, Title = "Fundación", AuthorId = 4, CategoryId = 2 },
                    new Book { Id = 10, Title = "Yo, Robot", AuthorId = 4, CategoryId = 2 },

                    // Agatha Christie
                    new Book { Id = 11, Title = "Asesinato en el Orient Express", AuthorId = 5, CategoryId = 5 },
                    new Book { Id = 12, Title = "Diez Negritos", AuthorId = 5, CategoryId = 5 },

                    // Stephen King
                    new Book { Id = 13, Title = "It", AuthorId = 6, CategoryId = 6 },
                    new Book { Id = 14, Title = "El Resplandor", AuthorId = 6, CategoryId = 6 },
                    new Book { Id = 15, Title = "Cementerio de Animales", AuthorId = 6, CategoryId = 6 }
                );

        }
    }
}
