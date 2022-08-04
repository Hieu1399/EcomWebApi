using EcomWebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcomWebAPI.IRepository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductList();
        Task<Product> GetProductById(int productId);
        Task<IEnumerable<Product>> GetProductByName(string productName);
        Task<IEnumerable<Product>> GetProductByCategory(int categoryId);
        Task<bool> Create(Product product);
        Task <bool> Update(Product product);
        Task <bool> Delete(Product product);
        Task<bool>  ProductAddToCart(int productId);
    }
}
