using EcomWebAPI.Data;
using EcomWebAPI.IRepository;
using EcomWebAPI.Repository;
using EcomWebAPI.Repository.IRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;


    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly EcomDbContext _db;
        private readonly ILogger _logger;

        public IProductRepository productRepository { get; private set; }
        public ICategoryRepository categoryRepository { get; private set; }
        public IUserRepository userRepository { get; private set; }

    public async Task CompleteAsync()
        {
            await _db.SaveChangesAsync();
        }
        public UnitOfWork(EcomDbContext db,ILoggerFactory loggerFactory)
        {
            _db = db;
            _logger = loggerFactory.CreateLogger("logs");

            productRepository = new ProductRepository(_db);
            categoryRepository = new CategoryRepository(_db);
            userRepository = new UserRepository(_db);
        }
        public void Dispose()
        {
            _db.Dispose();
        }
        
    }

