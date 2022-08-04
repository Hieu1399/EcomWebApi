namespace EcomWebAPI.Service.Actors.Messaging
{
    public class UpdateItemQuantityMsg : CartOperationMsg
    {
        public readonly int ProductId;

        public readonly int Quantity;

        public UpdateItemQuantityMsg(int customerId, int productId, int quantity) : base(customerId)
        {
            this.ProductId = productId;
            this.Quantity = quantity;
        }
    }
}
