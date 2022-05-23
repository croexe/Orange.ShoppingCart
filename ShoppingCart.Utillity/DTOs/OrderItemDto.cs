namespace ShoppingCart.Utillity.DTOs
{
    public class OrderItemDto
    {
        public int OrderItemId { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
    }
}