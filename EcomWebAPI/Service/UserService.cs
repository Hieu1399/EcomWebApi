using EcomWebAPI.Models.User;
using EcomWebAPI.Service.IService;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcomWebAPI.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService> _logger;
        public UserService(ILogger<UserService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateCustomer(User user)
        {
            await _unitOfWork.userRepository.CreateCustomer(user);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        public async Task<bool> CreateAdmin(User user)
        {
            await _unitOfWork.userRepository.CreateAdmin(user);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> Delete(User user)
        {
            await _unitOfWork.userRepository.Delete(user);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _unitOfWork.userRepository.GetUserById(userId);
        }

        public async Task<IEnumerable<User>> GetUserList()
        {
            return await _unitOfWork.userRepository.GetUserList();
        }

        public async Task<bool> UserExist(string username)
        {
             return await _unitOfWork.userRepository.UserExist(username);
        }
    }
}
