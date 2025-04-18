﻿
namespace DuoWord.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public StartupSetup _dbContext;
        public CategoryRepository(StartupSetup dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Category> Create(Category category)
        {
            await this._dbContext.Categories.AddAsync(category);
            await this._dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<int> Delete(Guid id)
        {
            return await this._dbContext.Categories.Where(category => category.Id == id).ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<Category>> SelectAll()
        {
            return await this._dbContext.Categories.ToListAsync();
        }

        public async Task<Category> SelectById(Guid id)
        {
            return await this._dbContext.Categories.SingleAsync(category => category.Id == id);
        }

        public async Task<Category?> SelectByName(string name)
        {
            return await this._dbContext.Categories.SingleOrDefaultAsync(categ => categ.Name == name);
        }

        public async Task<Category> Update(Category category)
        {
            var categorySelected = await this._dbContext.Categories.SingleAsync(categ => categ.Id == category.Id);
            categorySelected.State = string.IsNullOrEmpty(category.State) ? categorySelected.State : category.State;
            categorySelected.Name = string.IsNullOrEmpty(category.Name) ? categorySelected.Name : category.State;
            await this._dbContext.SaveChangesAsync();

            return categorySelected;
        }
    }
}
