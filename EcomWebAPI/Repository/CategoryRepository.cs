using EcomWebAPI.Data;
using EcomWebAPI.IRepository;
using EcomWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcomWebAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EcomDbContext _db;

        public CategoryRepository(EcomDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(Category category)
        {
            await _db.Categories.AddAsync(category);
            return true;
        }

        public async Task<bool> Delete(Category category)
        {
            var exist = await _db.Categories.Where(x => x.Id == category.Id).FirstOrDefaultAsync();
            if (exist != null)
            {
                _db.Categories.Remove(exist);
                return true;
            }
            return false;
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            return await _db.Categories.FindAsync(categoryId);
        }

        public async Task<IEnumerable<Category>> GetCategoryList()
        {
            return await _db.Categories.OrderBy(a => a.Id).ToListAsync();
        }

        public async Task<bool> Update(Category category)
        {
            var exist = await _db.Products.Where(x => x.Id == category.Id).FirstOrDefaultAsync();
            if (exist == null)
            {
                return await Create(category);
            }
            exist.Name = category.Name;
            exist.Description = category.Description;

            return true;
        }
    }
}
