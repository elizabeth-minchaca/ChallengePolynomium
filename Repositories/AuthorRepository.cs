using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChallengePolynomius.Configurations;
using ChallengePolynomius.DTOs;
using ChallengePolynomius.Models;
using ChallengePolynomius.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ChallengePolynomius.Repositories
{
    public class AuthorRepository: IAuthorRepository
    {
        private readonly LibraryContext _context;
        private readonly IMapper _mapper;

        public AuthorRepository(LibraryContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<AuthorGetDTO>> GetAuthorsList()
        {
            var authorFilter = new AuthorFilterDTO();
            return await this.GetResultFilter(authorFilter).ProjectTo<AuthorGetDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
        public async Task<AuthorGetDTO> GetAuthorByIdAsync(int id)
        {
            return await _context.Authors
                .Where(a => a.Id == id)
                .ProjectTo<AuthorGetDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }
        public async Task<AuthorGetDTO> GetAuthorByFilterAsync(AuthorFilterDTO authorFilter)
        {
            return await this.GetResultFilter(authorFilter).ProjectTo<AuthorGetDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }

        public async Task<AuthorGetDTO> AddAuthorAsync(AuthorPostDTO authorPostDTO)
        {
            Author author = _mapper.Map<Author>(authorPostDTO);
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            return await this.GetAuthorByFilterAsync(new AuthorFilterDTO { Id = author.Id});
        }

        public async Task<AuthorGetDTO> UpdateAuthorAsync(AuthorEditDTO authorEditDTO)
        {
            Author entityToEdit = await _context.Authors.FirstOrDefaultAsync(a => a.Id == authorEditDTO.Id);

            if (entityToEdit is null)
            {
                throw new Exception("Autor no encontrado");
            }

            _mapper.Map(authorEditDTO, entityToEdit);

            await _context.SaveChangesAsync(); // Guarda los cambios en la entidad ya rastreada

            return await this.GetAuthorByFilterAsync(new AuthorFilterDTO { Id = entityToEdit.Id });
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var entityToDelete = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);

            if (entityToDelete is null)
            {
                throw new Exception("Autor no encontrado");
            }

            _context.Authors.Remove(entityToDelete);
            return await _context.SaveChangesAsync() > 0;
        }



        private IQueryable<Author> GetResultFilter(AuthorFilterDTO authorFilter)
        {
            var query = _context.Authors.AsQueryable();

            if (authorFilter.Id.HasValue)
            {
                query = query.Where(a => a.Id == authorFilter.Id.Value);
            }

            if (!string.IsNullOrEmpty(authorFilter.Name))
            {
                query = query.Where(c => c.Name.ToLower().Contains(authorFilter.Name.ToLower()));
            }

            return query;
        }



    }
}
