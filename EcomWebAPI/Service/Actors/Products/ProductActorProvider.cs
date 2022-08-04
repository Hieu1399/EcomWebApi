using Akka.Actor;
using EcomWebAPI.Data;
using EcomWebAPI.Models;
using EcomWebAPI.Service.IService;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcomWebAPI.Service.Actors.Products
{
    public class ProductActorProvider
    {
        private IActorRef _ProductsActor { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        private readonly EcomDbContext _db;
        private readonly IProductService _productservice;
        public ProductActorProvider(EcomDbContext db , IActorRef ProductsActor, IUnitOfWork unitOfWork)
        {
            _db = db;
            _ProductsActor = ProductsActor;
            _unitOfWork = unitOfWork;
        }

        public ProductActorProvider(ActorSystem actorSystem)
        {
            var products = Data.ProductEx.GetProducts();
            _ProductsActor = actorSystem.ActorOf(Props.Create<ProductActor>(products), "products");
        }
        public IActorRef GetInstance()
        {
            return _ProductsActor;
        }
    }   
}
