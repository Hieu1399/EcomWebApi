namespace EcomWebAPI.Service.Actors.Messaging
{
    public class ClearCartMsg : CartOperationMsg
    {
        public ClearCartMsg(int customerId) : base(customerId)
        {

        }
    }
}
