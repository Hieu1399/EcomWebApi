using EcomWebAPI.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcomWebAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUserList();
        Task<User> GetUserById(int userId);
        Task<bool> CreateCustomer(User user);
        Task<bool> CreateAdmin(User user);
        Task<bool> Delete(User user);
        Task<bool> UserExist(string username);
    }
}
