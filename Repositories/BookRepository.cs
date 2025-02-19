using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChallengePolynomius.Configurations;
using ChallengePolynomius.DTOs;
using ChallengePolynomius.Models;
using ChallengePolynomius.Repositories.Interfaces;
using ChallengePolynomius.Utils;
using Microsoft.EntityFrameworkCore;

namespace ChallengePolynomius.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _context;
        private readonly IMapper _mapper;
        //Recibe el contexto y el mapper por inyección de dependencias.
        public BookRepository(LibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BookGetDTO>> GetBooksList()
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .ProjectTo<BookGetDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
        public async Task<PagedResult<BookGetDTO>> GetBooksAsync(BookFilterNameDTO filter)
        {
            // Inicia la consulta incluyendo las relaciones con autor y categoría
            var query = _context.Books.Include(b => b.Author).Include(b => b.Category).AsQueryable();

            // Filtra por ID, si se proporcionó
            if (filter.Id.HasValue)
                query = query.Where(b => b.Id == filter.Id.Value);

            // Filtra por título, si se proporcionó
            if (!string.IsNullOrEmpty(filter.Title))
                query = query.Where(b => b.Title.ToLower().Contains(filter.Title.ToLower())); // Filtro insensible a mayúsculas y minúsculas

            // Filtra por nombre de autor, si se proporcionó
            if (!string.IsNullOrEmpty(filter.AuthorName))
                query = query.Where(b => b.Author.Name.ToLower().Contains(filter.AuthorName.ToLower())); // Filtro insensible a mayúsculas y minúsculas

            // Filtra por nombre de categoría, si se proporcionó
            if (!string.IsNullOrEmpty(filter.CategoryName))
                query = query.Where(b => b.Category.Name.ToLower().Contains(filter.CategoryName.ToLower())); // Filtro insensible a mayúsculas y minúsculas

            // Contabiliza el total de registros que cumplen con los filtros
            var totalRecords = await query.CountAsync();

            // Aplica la paginación
            var books = await query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ProjectTo<BookGetDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            // Retorna el resultado paginado
            return new PagedResult<BookGetDTO>(books, totalRecords, filter.Page, filter.PageSize);
        }


        public async Task<BookGetDTO> GetBookByFilterAsync(BookFilterDTO bookFilter)
        {
            var query = _context.Books.AsQueryable();

            if (bookFilter.Id.HasValue)
            {
                query = query.Where(b => b.Id == bookFilter.Id.Value);
            }

            if (!string.IsNullOrEmpty(bookFilter.Title))
            {
                query = query.Where(b => b.Title.Contains(bookFilter.Title));
            }

            if (bookFilter.AuthorId.HasValue)
            {
                query = query.Where(b => b.AuthorId == bookFilter.AuthorId.Value);
            }

            if (bookFilter.CategoryId.HasValue)
            {
                query = query.Where(b => b.CategoryId == bookFilter.CategoryId.Value);
            }

            var result = await query
                .Include(b => b.Author)
                .Include(b => b.Category)
                .FirstOrDefaultAsync();

            return _mapper.Map<BookGetDTO>(result);
        }

        public async Task<BookGetDTO> GetBookByIdAsync(int id)
        {
            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.Id == id);

            return _mapper.Map<BookGetDTO>(book);
        }

        public async Task<BookGetDTO> AddBookAsync(BookPostDTO bookPostDTO)
        {
            var book = _mapper.Map<Book>(bookPostDTO);
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();

            return await GetBookByIdAsync(book.Id);
        }

        public async Task<BookGetDTO> UpdateBookAsync(BookEditDTO bookEditDTO)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == bookEditDTO.Id);

            if (book == null)
            {
                throw new Exception("Book not found");
            }

            _mapper.Map(bookEditDTO, book);
            await _context.SaveChangesAsync();

            return _mapper.Map<BookGetDTO>(book);
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                throw new Exception("Book not found");
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
