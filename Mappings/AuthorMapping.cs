using AutoMapper;
using ChallengePolynomius.DTOs;
using ChallengePolynomius.Models;

namespace ChallengePolynomius.Mappings
{
    public class AuthorMapping: Profile
    {
        public AuthorMapping()
        {
            CreateMap<AuthorPostDTO, Author>();
            CreateMap<AuthorEditDTO, Author>();
            CreateMap<Author, AuthorGetDTO>().ReverseMap();

        }
    }
}
