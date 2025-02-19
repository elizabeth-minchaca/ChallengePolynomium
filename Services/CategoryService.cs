using AutoMapper;
using ChallengePolynomius.Configurations;
using ChallengePolynomius.DTOs;
using ChallengePolynomius.Models;
using ChallengePolynomius.Repositories.Interfaces;
using ChallengePolynomius.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChallengePolynomius.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryGetDTO>> GetCategoriesList()
        {
            return await _categoryRepository.GetCategoriesList();
        }

        public async Task<CategoryGetDTO> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetCategoryByIdAsync(id);
        }

        public async Task<CategoryGetDTO> GetCategoryByFilterAsync(CategoryFilterDTO categoryFilter)
        {
            return await _categoryRepository.GetCategoryByFilterAsync(categoryFilter);
        }

        public async Task<CategoryGetDTO> AddCategoryAsync(CategoryPostDTO categoryPostDTO)
        {
            return await _categoryRepository.AddCategoryAsync(categoryPostDTO);
        }

        public async Task<CategoryGetDTO> UpdateCategoryAsync(CategoryEditDTO categoryEditDTO)
        {
            return await _categoryRepository.UpdateCategoryAsync(categoryEditDTO);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            return await _categoryRepository.DeleteCategoryAsync(id);
        }
    }
}
