using ChallengePolynomius.DTOs;
using ChallengePolynomius.Models;

namespace ChallengePolynomius.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryGetDTO>> GetCategoriesList();
        Task<CategoryGetDTO> GetCategoryByFilterAsync(CategoryFilterDTO categoryFilter);
        Task<CategoryGetDTO> GetCategoryByIdAsync(int id);
        Task<CategoryGetDTO> AddCategoryAsync(CategoryPostDTO category);
        Task<CategoryGetDTO> UpdateCategoryAsync(CategoryEditDTO category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
