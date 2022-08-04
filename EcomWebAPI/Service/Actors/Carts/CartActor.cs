using Akka.Actor;
using EcomWebAPI.Models;
using EcomWebAPI.Service.Actors.Messaging;
using System;
using System.Threading.Tasks;

namespace EcomWebAPI.Service.Actors.Carts
{
    public class CartActor : ReceiveActor
    {
        public Cart cartSate { get; set; }

        private IActorRef ProductsActorRef { get; set; }

        public CartActor(IActorRef productsActor)
        {
            this.cartSate = new Cart();
            this.ProductsActorRef = productsActor;

            Receive<GetCartMsg>(_ => Sender.Tell(this.cartSate));
            ReceiveAsync<AddItemToCartMsg>(m => AddItemToBasketActionAsync(m).PipeTo(Sender), m => m.Quantity > 0);
            ReceiveAsync<UpdateItemQuantityMsg>(m => UpdateItemQuantityToBasketActionAsync(m).PipeTo(Sender), m => m.Quantity > 0);
            Receive<RemoveItemFromCartMsg>(m => Sender.Tell(RemoveItemToBasketActionAsync(m)));
            Receive<ClearCartMsg>(m => Sender.Tell(ClearBasketActionAsync(m)));
        }

        private async Task<Cart> AddItemToBasketActionAsync(AddItemToCartMsg message)
        {
            var result = await this.ProductsActorRef.Ask<Models.Status>(
                new UpdateProductStockMsg(
                    productId: message.ProductId,
                    quantity: message.Quantity
                )
            );

            switch (result)
            {
                case Models.Status.StockUpdated:
                    return await AddItemToBasketAsync(message.CustomerId, message.ProductId, message.Quantity);
                default: // insufficient stock
                    return this.cartSate; // return current basket
            }

        }

        private async Task<Cart> AddItemToBasketAsync(int customerId, int productId, int quantity)
        {

            // check if product was added previously
            var existingProduct = this.cartSate.Items?.Find(item => item.Product.Id == productId);

            if (existingProduct != null && existingProduct is CartItem)
            {
                // calculate increase in price
                var increaseInAmount = this.cartSate.Subtotal + (existingProduct.Product.UnitPrice * quantity);

                existingProduct.Quantity = existingProduct.Quantity + quantity;

                // calculate increase in total items
                var totalItems = this.cartSate.TotalItems + quantity;

                this.cartSate.Subtotal = increaseInAmount;
                this.cartSate.TotalItems = totalItems;

                return this.cartSate;
            }
            else
            {

                var product = await this.ProductsActorRef.Ask<Product>(new GetProductMsg(productId));

                if (product != null && product is Product)
                {
                    // reinit basket total items and total price
                    this.cartSate.TotalItems = this.cartSate.TotalItems + quantity;
                    this.cartSate.Id = Guid.NewGuid();
                    this.cartSate.CustomerId = customerId;
                    this.cartSate.Subtotal = this.cartSate.Subtotal + (quantity * product.UnitPrice);

                    // create a new basket item/product
                    this.cartSate.Items.Add(new CartItem
                    {
                        Product = product,
                        Quantity = quantity,
                    });
                }

                return this.cartSate;
            }
        }

        private async Task<Cart> UpdateItemQuantityToBasketActionAsync(UpdateItemQuantityMsg message)
        {
            // check if product was added previously
            var existingProduct = this.cartSate.Items?.Find(item => item.Product.Id == message.ProductId);

            if (existingProduct != null && existingProduct is CartItem)
            {
                var previousQuantity = existingProduct.Quantity;

                var newQuantity = message.Quantity;

                var quantityDiff = newQuantity - previousQuantity;

                // update product stock
                var result = await this.ProductsActorRef.Ask<Models.Status>(
                    new UpdateProductStockMsg(
                        productId: message.ProductId,
                        quantity: quantityDiff
                    )
                );

                switch (result)
                {
                    case Models.Status.StockUpdated:
                        // calculate new price
                        var newAmount = this.cartSate.Subtotal + (existingProduct.Product.UnitPrice * quantityDiff);

                        // calculate new quantity
                        existingProduct.Quantity = existingProduct.Quantity + quantityDiff;

                        // calculate increase in total items
                        var totalItems = this.cartSate.TotalItems + quantityDiff;

                        this.cartSate.Subtotal = newAmount;
                        this.cartSate.TotalItems = totalItems;

                        return this.cartSate;

                    default: // insufficient stock
                        return this.cartSate;
                }
            }

            return this.cartSate;
        }

        public async Task<bool> RemoveItemToBasketActionAsync(RemoveItemFromCartMsg message)
        {
            var cartItem = this.cartSate.Items?.Find(item => item.Product.Id == message.ProductId);

            if (cartItem != null && cartItem is CartItem)
            {
                var itemRemoved = this.cartSate.Items.Remove(cartItem);
                if (itemRemoved)
                {
                    // reinit basket total items and total price
                    this.cartSate.TotalItems = this.cartSate.TotalItems - cartItem.Quantity;
                    this.cartSate.Subtotal = this.cartSate.Subtotal - (cartItem.Quantity * cartItem.Product.UnitPrice);

                    // update stock
                    var result = await this.ProductsActorRef.Ask<Models.Status>(
                       new UpdateProductStockMsg(
                           productId: message.ProductId,
                           quantity: -cartItem.Quantity
                       )
                     );

                    switch (result)
                    {
                        case Models.Status.StockUpdated:
                            return true;
                        default:
                            return false;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        public async Task<Cart> ClearBasketActionAsync(ClearCartMsg message)
        {
            if (this.cartSate.CustomerId == message.CustomerId)
            {
                if (this.cartSate.Items?.Count >= 0)
                {
                    foreach (var item in this.cartSate.Items)
                    {
                        // update stock for each item
                        var result = await this.ProductsActorRef.Ask<Models.Status>(
                           new UpdateProductStockMsg(
                               productId: item.Product.Id,
                               quantity: -item.Quantity
                           )
                         );
                    }
                }

                // clear items
                this.cartSate.Items?.Clear();

                // reinit basket total items and total price
                this.cartSate.TotalItems = 0;
                this.cartSate.Subtotal = 0;

            }

            // return empty basket
            return this.cartSate;
        }

        public static Props Props(IActorRef productsActor)
        {
            return Akka.Actor.Props.Create(() => new CartActor(productsActor));
        }
    }
}
