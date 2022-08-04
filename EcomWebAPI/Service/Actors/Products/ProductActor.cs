using Akka.Actor;
using System.Collections.Generic;
using EcomWebAPI.Models;
using EcomWebAPI.Service.Actors.Messaging;
using System.Linq;
using EcomWebAPI.Data;

namespace EcomWebAPI.Service.Actors.Products
{
    public class ProductActor : ReceiveActor
    {
        private IList<Product> Products { get; set; }

        private readonly EcomDbContext _db;

        public ProductActor(IList<Product> products, EcomDbContext db)
        {
            this.Products = products;
            this._db = db;

            // receive get a product catalog message, return required product
            Receive<GetProductMsg>(m => Sender.Tell(GetProductById(m)));

            // receive update stock message, trigger action UpdateStockAction
            Receive<UpdateProductStockMsg>(m => Sender.Tell(UpdateStockAction(m)));
        }

        public Models.Status UpdateStockAction(UpdateProductStockMsg message)
        {
            var product = _db.Products
                .FirstOrDefault(p => p.Id == message.ProductId);

            if (product is Product && product != null)
            {
                if (product.UnitsInStock >= 0)
                {
                    // update quantity
                    var quantityAvailable = product.UnitsInStock - message.Quantity;

                    if (quantityAvailable >= 0)
                    {
                        product.UnitsInStock = quantityAvailable;
                        return Models.Status.StockUpdated;
                    }
                    else
                    {
                        return Models.Status.InsuffientStock;
                    }
                }
                else
                {
                    return Models.Status.InsuffientStock;
                }
            }

            return Models.Status.ItemNotFound;
        }

        public Product GetProductById(GetProductMsg message)
        {
            var product = _db.Products
                .FirstOrDefault(p => p.Id == message.ProductId);

            if (product != null && product is Product)
            {
                return product;
            }

            return null;
        }
    }
}
