using AutoMapper;
using Flashcards.Domain.Commands;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Responses;

namespace Flashcards.Infrastructure.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserAddCommand, UserEntity>().ForMember(dest => dest.Email, opt => opt.AddTransform(opt => opt.ToLower().Trim()));
            CreateMap<UserEntity, UserGetResponse>();
        }
    }
}
