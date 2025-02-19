using AutoMapper;
using ChallengePolynomius.DTOs;
using ChallengePolynomius.Models;

namespace ChallengePolynomius.Mappings
{
    public class BookMapping : Profile
    {
        //El constructor define todas las reglas de conversión entre los modelos y los DTOs.
        public BookMapping()
        {
            CreateMap<BookPostDTO, Book>();
            CreateMap<BookEditDTO, Book>();
            //Se usa .ForMember() para mapear propiedades que no existen directamente en Book
            CreateMap<Book, BookGetDTO>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ReverseMap();
        }
    }
}
