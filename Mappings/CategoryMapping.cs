using AutoMapper;
using ChallengePolynomius.DTOs;
using ChallengePolynomius.Models;

namespace ChallengePolynomius.Mappings
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<CategoryPostDTO, Category>();
            CreateMap<CategoryEditDTO, Category>();
            CreateMap<Category, CategoryGetDTO>().ReverseMap();
        }
    }
}
