using EcomWebAPI.Data;
using EcomWebAPI.Models.User;
using EcomWebAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcomWebAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly EcomDbContext _db;

        public UserRepository(EcomDbContext db)
        {
            _db = db;
        }
        public async Task<bool> CreateCustomer(User user)
        {
            user.Role = "Customer";
            await _db.UserModels.AddAsync(user);
            return true;
        }

        public async Task<bool> CreateAdmin(User user)
        {
            user.Role = "Adminstrator";
            await _db.UserModels.AddAsync(user);
            return true;
        }

        public async Task<bool> Delete(User user)
        {
            var exist = await _db.UserModels.Where(x => x.Id == user.Id).FirstOrDefaultAsync();
            if (exist != null)
            {
                _db.UserModels.Remove(exist);
                return true;
            }
            return false;
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _db.UserModels.FindAsync(userId);
        }

        public async Task<IEnumerable<User>> GetUserList()
        {
            return await _db.UserModels.OrderBy(a => a.Id).ToListAsync();
        }

        public async Task<bool> UserExist(string username)
        {
            bool value = await _db.UserModels.AnyAsync(a => a.Username.ToLower().Trim() == username.ToLower().Trim());
            return value;
        }
    }
}
