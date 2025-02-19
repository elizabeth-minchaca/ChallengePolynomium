using AutoMapper;
using ChallengePolynomius.DTOs;
using ChallengePolynomius.Models;
using ChallengePolynomius.Repositories.Interfaces;
using ChallengePolynomius.Services.Interfaces;
using ChallengePolynomius.Utils;

namespace ChallengePolynomius.Services
{
    public class BookService: IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        /*public async Task<PagedResult<BookGetDTO>> GetBooksAsync(BookFilterNameDTO filter)
        {
            return await _bookRepository.GetBooksAsync(filter);
        }*/
        public async Task<IEnumerable<BookGetDTO>> GetBooksList()
        {
            return await _bookRepository.GetBooksList();
        }

        public async Task<BookGetDTO> GetBookByIdAsync(int id)
        {
            return await _bookRepository.GetBookByIdAsync(id);
        }

        public async Task<BookGetDTO> GetBookByFilterAsync(BookFilterDTO bookFilter)
        {
            return await _bookRepository.GetBookByFilterAsync(bookFilter);
        }

        public async Task<BookGetDTO> AddBookAsync(BookPostDTO bookPostDTO)
        {
            return await _bookRepository.AddBookAsync(bookPostDTO);
        }

        public async Task<BookGetDTO> UpdateBookAsync(BookEditDTO bookEditDTO)
        {
            return await _bookRepository.UpdateBookAsync(bookEditDTO);
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            return await _bookRepository.DeleteBookAsync(id);
        }
    }
}
