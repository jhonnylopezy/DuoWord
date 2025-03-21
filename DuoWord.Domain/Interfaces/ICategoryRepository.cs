
namespace DuoWord.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> Create(Category category);
        Task<IEnumerable<Category>> SelectAll();
        Task<Category> Update(Category category);
        Task<int> Delete(Guid id);
        Task<Category> SelectById(Guid id);
        Task<Category?> SelectByName(string name);
    }
}
