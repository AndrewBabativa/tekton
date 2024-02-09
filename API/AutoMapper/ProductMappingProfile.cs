using AutoMapper;
using Core.Commands; 
using Infrastructure.Models; 

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<CreateProductCommand, Product>();
    }
}
