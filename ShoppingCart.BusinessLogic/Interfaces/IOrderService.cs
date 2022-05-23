using ShoppingCart.Utillity.DTOs;

namespace ShoppingCart.BusinessLogic.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> GetOrderAsync(int orderId);
        Task<OrderDto> GetOrderWithAllOrderItemsAsync(int orderId);
        Task<ICollection<OrderDto>> GetOrdersAsync();
        Task<OrderDto> CreateOrderAsync(OrderDto orderDto);
        Task<OrderDto> UpdateOrderAsync(OrderDto orderDto);
        Task<OrderDto> PatchOrderAsync(OrderDto orderDto);
        Task DeleteOrderAsync(int orderId);
    }
}