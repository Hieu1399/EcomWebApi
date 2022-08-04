using EcomWebAPI.Data;
using EcomWebAPI.IRepository;
using EcomWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcomWebAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcomDbContext _db;

        protected readonly ILogger _logger;
        public ProductRepository(EcomDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Product product)
        {
            await _db.Products.AddAsync(product);
            return true ;
        }

        public async Task<bool> Delete(Product product)
        {
            try
            {
                var exist = await _db.Products.Where(x => x.Id == product.Id).FirstOrDefaultAsync();
                if (exist != null)
                {
                    _db.Products.Remove(exist);
                    return true ;
                }
                return false;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "{Repo} Delete method Error", typeof(ProductRepository));
                return false;
            }
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(int categoryId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Product> GetProductById(int productId)
        {
            return await _db.Products.FindAsync(productId);
        }

        public async Task<IEnumerable<Product>> GetProductByName(string productName)
        {
            return (IEnumerable<Product>)await _db.Products.FindAsync(productName);
        }

        public async Task<IEnumerable<Product>> GetProductList()
        {
            try
            {
                return await _db.Products.Include(c => c.Category).OrderBy(a => a.Id).ToListAsync();
            }
            catch(Exception ex)
            {
                //_logger.LogError(ex, "{Repo} All method Error", typeof(ProductRepository));
                return new List<Product>();
            }            
        }

        public async Task<bool> Update(Product product)
        {
            try
            {
                var existingProduct = await _db.Products.Where(x => x.Id == product.Id).FirstOrDefaultAsync();
                if (existingProduct == null)
                {
                    return await Create(product);
                }
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.ImageFile = product.ImageFile;
                existingProduct.UnitPrice = product.UnitPrice;
                existingProduct.UnitsInStock = product.UnitsInStock;
                existingProduct.CategoryId = product.CategoryId; 
                
                return true;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "{Repo} Update method Error", typeof(ProductRepository));
                return false;
            }
        }

        public async Task<bool> ProductAddToCart(int productId)
        {
            await _db.Products.Where(p => p.Id == productId).FirstOrDefaultAsync();

            return true;
        }
        
    }
}
