using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ShoppingCart.BusinessLogic.Interfaces;
using ShoppingCart.Domain.Models;
using ShoppingCart.Infrastructure.Database;
using ShoppingCart.Utillity.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.BusinessLogic.Implementation
{
    public class OrderRepository : IDisposable, IOrderService
    {
        private readonly CartDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public OrderRepository(CartDbContext context, IMapper mapper, ILogger logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<OrderDto> GetOrderAsync(int orderId)
        {
            OrderDto orderDto = null;
            try
            {
                var order = await _context.Orders.SingleAsync(o => o.OrderId == orderId);
                orderDto = _mapper.Map<Order, OrderDto>(order);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
            return orderDto;
        }

        public async Task<OrderDto> GetOrderWithAllOrderItemsAsync(int orderId)
        {
            OrderDto orderDto = null;
            try
            {
                var order = await _context.Orders.Include(o => o.Items).SingleOrDefaultAsync(o => o.OrderId == orderId);
                orderDto = _mapper.Map<Order, OrderDto>(order);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
            return orderDto;
        }

        public async Task<ICollection<OrderDto>> GetOrdersAsync()
        {
            ICollection<OrderDto> orderDtos = new Collection<OrderDto>();
            try
            {
                var orders = await _context.Orders.ToListAsync();
                foreach (var order in orders)
                {
                    var orderDto = _mapper.Map<Order, OrderDto>(order);
                    orderDtos.Add(orderDto);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
            return orderDtos;
        }

        public async Task<OrderDto> CreateOrderAsync(OrderDto orderDto)
        {
            Order order;
            try
            {
                order = _mapper.Map<OrderDto, Order>(orderDto);
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
            return orderDto;
        }

        public async Task<OrderDto> UpdateOrderAsync(OrderDto orderDto)
        {
            Order order;
            try
            {

                order = _mapper.Map<OrderDto, Order>(orderDto);
                var trackedOrder = _context.Attach(order);
                trackedOrder.State = EntityState.Modified;
                await _context.SaveChangesAsync();
                /*
                var orderToUpdate = _mapper.Map<OrderDto, Order>(orderDto);

                _context.Orders.Update(orderToUpdate);
                await _context.SaveChangesAsync();
                */
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
            return orderDto;
        }

        public async Task<OrderDto> PatchOrderAsync(OrderDto orderDto)
        {
            Order order = null;
            try
            {
                var forOrderItems = _mapper.Map<OrderDto, Order>(orderDto);
                order = await _context.Orders.Include(o => o.Items).SingleAsync(o => o.OrderId == orderDto.OrderId);
                order.ShippingAddress = orderDto.ShippingAddress;
                order.CustomerName = orderDto.CustomerName;
                order.Items = forOrderItems.Items;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
            return orderDto;
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            try
            {
                var order = await _context.Orders.Include(o => o.Items).SingleAsync(o => o.OrderId == orderId);
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }
    }
}
