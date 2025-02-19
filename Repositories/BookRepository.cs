using ChallengePolynomius.Configurations;
using ChallengePolynomius.Models;
using ChallengePolynomius.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChallengePolynomius.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _context;
        public BookRepository(LibraryContext context) { _context = context; }

        public async Task<IEnumerable<Book>> GetBooksAsync(string? title, int? authorId, int? categoryId, int page, int pageSize)
        {
            var query = _context.Books.Include(b => b.Author)
                .Include(b => b.Category)
                .AsQueryable();

            if (!string.IsNullOrEmpty(title)) query = query.Where(b => b.Title.Contains(title));

            if (authorId.HasValue) query = query.Where(b => b.AuthorId == authorId);

            if (categoryId.HasValue) query = query.Where(b => b.CategoryId == categoryId);

            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id) => await _context.Books.FindAsync(id);
        public async Task AddBookAsync(Book book) { await _context.Books.AddAsync(book); await _context.SaveChangesAsync(); }
        public async Task UpdateBookAsync(Book book) { _context.Books.Update(book); await _context.SaveChangesAsync(); }
        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null) { _context.Books.Remove(book); await _context.SaveChangesAsync(); }
        }
    }

}
