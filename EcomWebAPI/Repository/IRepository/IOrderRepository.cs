using EcomWebAPI.Models;
using System.Threading.Tasks;

namespace EcomWebAPI.IRepository
{
    public interface IOrderRepository
    {
        Task<Order> CheckOut(Order order);
    }
}
