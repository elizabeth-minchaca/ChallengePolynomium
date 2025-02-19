using ChallengePolynomius.DTOs;
using ChallengePolynomius.Models;

namespace ChallengePolynomius.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorGetDTO>> GetAuthorsList();
        Task<AuthorGetDTO> GetAuthorByFilterAsync(AuthorFilterDTO authorFilterDTO);
        Task<AuthorGetDTO> AddAuthorAsync(AuthorPostDTO author);
        Task<AuthorGetDTO> UpdateAuthorAsync(AuthorEditDTO author);
        Task<bool> DeleteAuthorAsync(int id);
    }
}
