namespace EcomWebAPI.Service.Actors.Messaging
{
    public class AddItemToCartMsg : CartOperationMsg
    {
        public readonly int ProductId;

        public readonly int Quantity;

        public AddItemToCartMsg(int customerId, int productId, int quantity) : base(customerId)
        {
            this.ProductId = productId;
            this.Quantity = quantity;
        }
    }
}
