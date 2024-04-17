using AutoMapper;
using VShop.ProductAPI.Models;

namespace VShop.ProductAPI.DTOs.Mappings
{
    public class MappingProfile : Profile
    {

        public MappingProfile() 
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
        
        }
    }
}
