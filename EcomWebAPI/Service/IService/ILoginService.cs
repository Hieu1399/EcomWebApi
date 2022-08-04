using EcomWebAPI.Models.User;
using System;
using System.Threading.Tasks;

namespace EcomWebAPI.Service.IService
{
    public interface ILoginService
    {
        string GenerateToken(User user);
        string GenerateRefreshToken(User user);
        User Authenticate(User userLogin);
    }
}
