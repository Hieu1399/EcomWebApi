using Akka.Actor;
using EcomWebAPI.Service.Actors.Products;

namespace EcomWebAPI.Service.Actors.Carts
{
    public class CartActorProvider
    {
        private IActorRef CartActorInstance { get; set; }

        public CartActorProvider(ActorSystem actorSystem, ProductActorProvider provider)
        {
            var productsActor = provider.GetInstance();
            this.CartActorInstance = actorSystem.ActorOf(CartActor.Props(productsActor), "cart");
        }

        public IActorRef GetInstance()
        {
            return this.CartActorInstance;
        }
    }
}
