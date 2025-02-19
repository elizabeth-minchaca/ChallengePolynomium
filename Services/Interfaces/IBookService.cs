using ChallengePolynomius.DTOs;
using ChallengePolynomius.Models;
using ChallengePolynomius.Utils;

namespace ChallengePolynomius.Services.Interfaces
{
    public interface IBookService
    {
        //Task<PagedResult<BookGetDTO>> GetBooksAsync(BookFilterNameDTO filter);
        Task<IEnumerable<BookGetDTO>> GetBooksList();
        Task<BookGetDTO> GetBookByIdAsync(int id);
        Task<BookGetDTO> GetBookByFilterAsync(BookFilterDTO bookFilter);
        Task<BookGetDTO> AddBookAsync(BookPostDTO book);
        Task<BookGetDTO> UpdateBookAsync(BookEditDTO book);
        Task<bool> DeleteBookAsync(int id);
    }
}
