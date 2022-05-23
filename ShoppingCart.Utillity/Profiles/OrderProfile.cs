using AutoMapper;
using ShoppingCart.Domain.Models;
using ShoppingCart.Utillity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Utillity.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderDto, Order>()
                .ForMember(dest => dest.OrderId,
                from => from.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.CustomerName,
                from => from.MapFrom(src => src.CustomerName))
                .ForMember(dest => dest.ShippingAddress,
                from => from.MapFrom(src => src.ShippingAddress))
                .ForMember(dest => dest.Items,
                from => from.MapFrom(src => src.Items))
                .ReverseMap();
        }
    }
}
