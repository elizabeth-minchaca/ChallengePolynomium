using ChallengePolynomius.Configurations;
using ChallengePolynomius.DTOs;
using ChallengePolynomius.Models;
using ChallengePolynomius.Repositories.Interfaces;
using ChallengePolynomius.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChallengePolynomius.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<AuthorGetDTO>> GetAuthorsList()
        {
            return await _authorRepository.GetAuthorsList();
        }
        public async Task<AuthorGetDTO> GetAuthorByIdAsync(int id)
        {
            return await _authorRepository.GetAuthorByIdAsync(id);
        }

        public async Task<AuthorGetDTO?> GetAuthorByFilterAsync(AuthorFilterDTO authorFilterDTO)
        {
            return await _authorRepository.GetAuthorByFilterAsync(authorFilterDTO);
        }

        public async Task<AuthorGetDTO> AddAuthorAsync(AuthorPostDTO author)
        {
            return await _authorRepository.AddAuthorAsync(author);
        }

        public async Task<AuthorGetDTO> UpdateAuthorAsync(AuthorEditDTO author)
        {
            return await _authorRepository.UpdateAuthorAsync(author);
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            return await _authorRepository.DeleteAuthorAsync(id);
        }
    }
}
