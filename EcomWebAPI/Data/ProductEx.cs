using EcomWebAPI.Models;
using System.Collections.Generic;

namespace EcomWebAPI.Data
{
    public class ProductEx
    {
        public static List<Product> GetProducts()
        {
            return new List<Product> {
                new Product {
                   Id = 1,
                   Name = "Iphone X",
                   UnitPrice = 50000,
                   UnitsInStock = 10000
                },
               new Product {
                   Id = 2,
                   Name = "Iphone 8",
                   UnitPrice = 40000,
                   UnitsInStock = 5000
                },
               new Product {
                   Id = 3,
                   Name = "Iphone 7",
                   UnitPrice = 35000,
                   UnitsInStock = 2500
                },
                new Product {
                   Id = 4,
                   Name = "Iphone 6S",
                   UnitPrice = 30000,
                   UnitsInStock = 1200
                },
                new Product {
                   Id = 5,
                   Name = "Iphone 6",
                   UnitPrice = 15000,
                   UnitsInStock = 5
                },
                new Product {
                   Id = 6,
                   Name = "Iphone 5s",
                   UnitPrice = 12000,
                   UnitsInStock = 0
                },
                new Product {
                   Id = 7,
                   Name = "Google Pixel 2",
                   UnitPrice = 42000,
                   UnitsInStock = 7000
                },
                new Product {
                   Id = 8,
                   Name = "Google Pixel",
                   UnitPrice = 22000,
                   UnitsInStock = 10
                },
            };
        }
    }
}
