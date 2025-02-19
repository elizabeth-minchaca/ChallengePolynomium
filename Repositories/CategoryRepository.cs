using AutoMapper.QueryableExtensions;
using AutoMapper;
using ChallengePolynomius.Configurations;
using ChallengePolynomius.DTOs;
using ChallengePolynomius.Models;
using ChallengePolynomius.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChallengePolynomius.Repositories
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly LibraryContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(LibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryGetDTO>> GetCategoriesList()
        {
            return await _context.Categories
                .ProjectTo<CategoryGetDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<CategoryGetDTO> GetCategoryByFilterAsync(CategoryFilterDTO categoryFilter)
        {
            var query = _context.Categories.AsQueryable();

            if (categoryFilter.Id.HasValue)
            {
                query = query.Where(c => c.Id == categoryFilter.Id.Value);
            }

            if (!string.IsNullOrEmpty(categoryFilter.Name))
            {
                query = query.Where(c => c.Name.ToLower().Contains(categoryFilter.Name.ToLower()));
            }

            var result = await query.FirstOrDefaultAsync();

            return _mapper.Map<CategoryGetDTO>(result);
        }


        public async Task<CategoryGetDTO> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);

            return _mapper.Map<CategoryGetDTO>(category);
        }

        public async Task<CategoryGetDTO> AddCategoryAsync(CategoryPostDTO categoryPostDTO)
        {
            var category = _mapper.Map<Category>(categoryPostDTO);
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return await GetCategoryByIdAsync(category.Id);
        }

        public async Task<CategoryGetDTO> UpdateCategoryAsync(CategoryEditDTO categoryEditDTO)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryEditDTO.Id);

            if (category == null)
            {
                throw new Exception("Categoría no encontrada.");
            }

            _mapper.Map(categoryEditDTO, category);
            await _context.SaveChangesAsync();

            return _mapper.Map<CategoryGetDTO>(category);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                throw new Exception("Categoría no encontrada.");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
