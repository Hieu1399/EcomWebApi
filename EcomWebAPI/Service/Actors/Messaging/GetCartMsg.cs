namespace EcomWebAPI.Service.Actors.Messaging
{
    public class GetCartMsg : CartOperationMsg
    {
        public GetCartMsg(int customerId) : base(customerId)
        {

        }
    }
}
