namespace EcomWebAPI.Service.Actors.Messaging
{
    public abstract class CartOperationMsg
    {

        public readonly int CustomerId;

        public CartOperationMsg(int customerId)
        {
            this.CustomerId = customerId;
        }
    }
}
