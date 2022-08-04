namespace EcomWebAPI.Service.Actors.Messaging
{
    public class GetProductMsg
    {
        public readonly int ProductId;

        public GetProductMsg(int productId)
        {
            this.ProductId = productId;
        }
    }
}
