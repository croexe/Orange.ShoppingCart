using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.BusinessLogic.Interfaces;
using ShoppingCart.Utillity.DTOs;

namespace ShoppingCart.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService orderService)
        {
            _service = orderService;
        }

        [HttpGet("single/{orderId}", Name ="GetOrder")]
        [ProducesResponseType(200, Type = typeof(OrderDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetOrder(int orderId)
        {
            var order = await _service.GetOrderAsync(orderId);
            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpGet("{orderId}", Name = "GetOrderWithItems")]
        [ProducesResponseType(200, Type = typeof(OrderDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetOrderWithItems(int orderId)
        {
            var order = await _service.GetOrderWithAllOrderItemsAsync(orderId);
            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpGet("all", Name = "GetOrders")]
        [ProducesResponseType(200, Type = typeof(OrderDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllOrders()
        {
            var order = await _service.GetOrdersAsync();
            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost("post")]
        [ProducesResponseType(201, Type = typeof(OrderDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostOrder(OrderDto orderDto)
        {
            OrderDto order;
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if(orderDto.OrderId == 0)
            {
                order = await _service.CreateOrderAsync(orderDto);
            } else {
                order = await _service.PatchOrderAsync(orderDto);
            }
            return Ok(order);
        }

        [HttpPut("{orderDto.orderId}")]
        [ProducesResponseType(200, Type = typeof(OrderDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateOrder(OrderDto orderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var order = await _service.UpdateOrderAsync(orderDto);

            return Ok(order);
        }

        [HttpDelete("{orderId}", Name = "DeleteOrder")]
        [ProducesResponseType(204, Type = typeof(OrderDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            await _service.DeleteOrderAsync(orderId);

            return Ok();
        }

        /*
        [HttpPatch("{orderDto.orderId}", Name = "PatchOrder")]
        [ProducesResponseType(200, Type = typeof(OrderDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PatchOrder(OrderDto orderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var order = await _service.PatchOrderAsync(orderDto);

            return Ok(order);
        }
*/
    }
}