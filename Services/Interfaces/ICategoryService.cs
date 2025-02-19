using ChallengePolynomius.DTOs;
using ChallengePolynomius.Models;

namespace ChallengePolynomius.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryGetDTO>> GetCategoriesList();
        Task<CategoryGetDTO> GetCategoryByIdAsync(int id);
        Task<CategoryGetDTO> GetCategoryByFilterAsync(CategoryFilterDTO categoryFilter);
        Task<CategoryGetDTO> AddCategoryAsync(CategoryPostDTO category);
        Task<CategoryGetDTO> UpdateCategoryAsync(CategoryEditDTO category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
