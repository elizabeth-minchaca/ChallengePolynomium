using AutoMapper;
using ChallengePolynomius.DTOs;
using ChallengePolynomius.Models;

namespace ChallengePolynomius.Mappings
{
    public class BookMapping : Profile
    {
        public BookMapping()
        {
            CreateMap<BookPostDTO, Book>();
            CreateMap<BookEditDTO, Book>();
            CreateMap<Book, BookGetDTO>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ReverseMap();
        }
    }
}
