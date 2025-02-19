using ChallengePolynomius.DTOs;
using ChallengePolynomius.Models;

namespace ChallengePolynomius.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<AuthorGetDTO>> GetAuthorsList();
        Task<AuthorGetDTO> GetAuthorByIdAsync(int id);
        Task<AuthorGetDTO> GetAuthorByFilterAsync(AuthorFilterDTO authorFilter);
        Task<AuthorGetDTO> AddAuthorAsync(AuthorPostDTO author);
        Task<AuthorGetDTO> UpdateAuthorAsync(AuthorEditDTO author);
        Task<bool> DeleteAuthorAsync(int id);
    }
}
