using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly ApplicationDbContext DbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await DbContext.Categories.AddAsync(category);
            await DbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> DeleteAsync(Guid id)
        {
            var existingCategory = await DbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (existingCategory is null)
            {
                return null;
            }

            DbContext.Categories.Remove(existingCategory);
            await DbContext.SaveChangesAsync();
            return existingCategory;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await DbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetById(Guid id)
        {
            return await DbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            var existingCategory = await DbContext.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);
            if (existingCategory != null)
            {
                DbContext.Entry(existingCategory).CurrentValues.SetValues(category);
                await DbContext.SaveChangesAsync();
                return category;
            }

            return null;
        }
    }
}
