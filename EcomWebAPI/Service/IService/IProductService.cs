using EcomWebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcomWebAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetProductById(int productId);
        Task<bool> Create(Product product);
        Task<bool> Update(Product product);
        Task<bool> Delete(Product product);
        Task<bool> ProductAddToCart(int productId);
    }
}
