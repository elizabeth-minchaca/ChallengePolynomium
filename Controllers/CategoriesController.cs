using ChallengePolynomius.DTOs;
using ChallengePolynomius.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChallengePolynomius.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetCategoriesList();
            return categories.Any() ? Ok(categories) : NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return category == null ? NotFound("Categoría no encontrada") : Ok(category);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetCategoryByFilter([FromQuery] CategoryFilterDTO categoryFilter)
        {
            var category = await _categoryService.GetCategoryByFilterAsync(categoryFilter);
            return category == null ? NotFound("Categoría no encontrada") : Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryPostDTO categoryPostDTO)
        {
            var result = await _categoryService.AddCategoryAsync(categoryPostDTO);
            return CreatedAtAction(nameof(GetCategoryById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryEditDTO categoryEditDTO)
        {
            if (id != categoryEditDTO.Id) return BadRequest("El ID de la categoría no coincide con el del cuerpo de la solicitud");

            var result = await _categoryService.UpdateCategoryAsync(categoryEditDTO);
            return result != null ? Ok(result) : NotFound("Categoría no encontrada");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var success = await _categoryService.DeleteCategoryAsync(id);
            return success ? NoContent() : NotFound("Categoría no encontrada");
        }
    }
}
