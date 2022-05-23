namespace ShoppingCart.Utillity.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string ShippingAddress { get; set; }
        public ICollection<OrderItemDto> Items { get; set; }

        public OrderDto()
        {

        }

        public OrderDto(int orderId, string customerName, string shippingAddress, ICollection<OrderItemDto> orderItemDtos)
        {
            OrderId = orderId;
            CustomerName = customerName;
            ShippingAddress = shippingAddress;
            Items = orderItemDtos;
        }
    }
}