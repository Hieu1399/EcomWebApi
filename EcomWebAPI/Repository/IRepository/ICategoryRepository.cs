using EcomWebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcomWebAPI.IRepository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoryList();
        Task<Category> GetCategoryById(int categoryId);
        Task<bool> Create(Category category);
        Task<bool> Update(Category category);
        Task<bool> Delete(Category category);
    }
}
