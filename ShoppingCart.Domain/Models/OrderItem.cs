using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Domain.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }

        [ForeignKey(name: "OrderId")]
        public Order Order { get; set; }
    }
}