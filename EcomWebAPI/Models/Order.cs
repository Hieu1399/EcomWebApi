using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcomWebAPI.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public PaymentMethodModel PaymentMethod { get; set; }
        public OrderStatusModel Status { get; set; }
        public decimal GrandTotal { get; set; }

        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
    public enum OrderStatusModel
    {
        Progress = 1,
        OnShipping = 2,
        Finished = 3
    }

    public enum PaymentMethodModel
    {
        Check = 1,
        BankTransfer = 2,
        Cash = 3,
        Paypal = 4,
        WalletPay = 5
    }
}
