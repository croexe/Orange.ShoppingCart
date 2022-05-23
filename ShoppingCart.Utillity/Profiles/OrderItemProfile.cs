using AutoMapper;
using ShoppingCart.Domain.Models;
using ShoppingCart.Utillity.DTOs;

namespace ShoppingCart.Utillity.Profiles
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<OrderItemDto, OrderItem>()
                .ForMember(dest => dest.OrderItemId,
                from => from.MapFrom(src => src.OrderItemId))
                .ForMember(dest => dest.ItemName,
                from => from.MapFrom(src => src.ItemName))
                .ForMember(dest => dest.Price,
                from => from.MapFrom(src => src.Price))
                .ForMember(dest => dest.Quantity,
                from => from.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.OrderId,
                from => from.MapFrom(src => src.OrderId))
                .ReverseMap();
        }
    }
}