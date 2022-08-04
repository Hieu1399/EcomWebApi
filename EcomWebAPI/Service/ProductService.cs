using EcomWebAPI.Models;
using EcomWebAPI.Service.IService;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcomWebAPI.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductService> _logger;
        public ProductService(ILogger<ProductService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Create(Product product)
        {
            await _unitOfWork.productRepository.Create(product);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> Delete(Product product)
        {
            await _unitOfWork.productRepository.Delete(product);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<Product> GetProductById(int productId)
        {
            return await _unitOfWork.productRepository.GetProductById(productId);
        }

        public async Task<bool> Update(Product product)
        {
            await _unitOfWork.productRepository.Update(product);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _unitOfWork.productRepository.GetProductList();
        }

        public async Task<bool> ProductAddToCart(int productId)
        {
            await _unitOfWork.productRepository.ProductAddToCart(productId);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
