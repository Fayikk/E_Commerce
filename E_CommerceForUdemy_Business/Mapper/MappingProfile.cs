using AutoMapper;
using E_CommerceForUdemy_DataAccess;
using E_CommerceForUdemy_DataAccess.ViewModel;
using ECommerce_ForUdemy_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceForUdemy_Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<ProductPrice, ProductPriceDTO>().ReverseMap();
            CreateMap<OrderHeader,OrderHeaderDTO>().ReverseMap();   
            CreateMap<OrderDetail,OrderDetailDTO>().ReverseMap();   
            CreateMap<Order,OrderDTO>().ReverseMap();
        }
    }
}
