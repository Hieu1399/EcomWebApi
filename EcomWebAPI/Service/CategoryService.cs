using EcomWebAPI.Models;
using EcomWebAPI.Service.IService;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcomWebAPI.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CategoryService> _logger;
        public CategoryService(ILogger<CategoryService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Create(Category category)
        {
            await _unitOfWork.categoryRepository.Create(category);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> Delete(Category category)
        {
            await _unitOfWork.categoryRepository.Delete(category);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            return await _unitOfWork.categoryRepository.GetCategoryById(categoryId);
        }

        public async Task<IEnumerable<Category>> GetCategoryList()
        {
            return await _unitOfWork.categoryRepository.GetCategoryList();
        }

        public async Task<bool> Update(Category category)
        {
            await _unitOfWork.categoryRepository.Update(category);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
