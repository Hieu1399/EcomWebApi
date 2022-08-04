using EcomWebAPI.IRepository;
using EcomWebAPI.Repository.IRepository;
using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        IProductRepository productRepository { get; }
        ICategoryRepository categoryRepository { get; }
        IUserRepository userRepository { get; }
        Task CompleteAsync();
    }
