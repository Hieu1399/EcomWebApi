namespace EcomWebAPI.Service.Actors.Messaging
{
    public class RemoveItemFromCartMsg : CartOperationMsg
    {
        public readonly int ProductId;

        public RemoveItemFromCartMsg(int customerId, int productId) : base(customerId)
        {
            this.ProductId = productId;
        }
    }
}
