using AutoMapper;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Store.Dto;
using Talabat.Core.Entities.Identity;

namespace Talabat.Store.Helpers
{
    public class MappingProfilies : Profile
    {
        public MappingProfilies()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductBrand, O => O.MapFrom(s => s.ProductBrand.Name)).
                ForMember(d => d.ProductType, O => O.MapFrom(s => s.ProductType.Name))

                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());

            CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<Core.Entities.Order_Aggregate.Address, AddressDto>().ReverseMap();

            CreateMap<OrderDto, Order>();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeleviryMethod, o => o.MapFrom(s => s.DeleviryMethod.ShortName))
                .ForMember(d => d.DeleviryMethodCost, o => o.MapFrom(s => s.DeleviryMethod.Cost));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(d=>d.PictureUrl,o=>o.MapFrom(s=>s.Product.PictureUrl))
                 .ForMember(d=>d.PictureUrl,o=>o.MapFrom<OrderPictureUrlResolver>());
           


        }
    }
}
