﻿using ChallengePolynomius.Configurations;
using ChallengePolynomius.Models;
using ChallengePolynomius.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChallengePolynomius.Repositories
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly LibraryContext _context;
        public CategoryRepository(LibraryContext context) { _context = context; }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
