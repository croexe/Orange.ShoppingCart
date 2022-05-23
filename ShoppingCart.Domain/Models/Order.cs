using System.Collections.ObjectModel;

namespace ShoppingCart.Domain.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string ShippingAddress { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new Collection<OrderItem>();
    }
}