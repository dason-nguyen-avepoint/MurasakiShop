using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MurasakiShop.Product.API.DTOs;
using MurasakiShop.Product.API.Models;

namespace MurasakiShop.Product.API.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // ProductImage mappings
        CreateMap<Models.Product, ProductDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category!.Name))
            .ForMember(dest => dest.MaterialName, opt => opt.MapFrom(src => src.Material!.Name));

        CreateMap<CreateProductDto, Models.Product>();
        CreateMap<UpdateProductDto, Models.Product>();

        // Category mappings
        CreateMap<Category, CategoryDto>();
        CreateMap<CreateCategoryDto, Category>();

        // Material mappings
        CreateMap<Material, MaterialDto>();

        // ProductImage mappings
        CreateMap<ProductImage, ProductImageDto>();
    }
}