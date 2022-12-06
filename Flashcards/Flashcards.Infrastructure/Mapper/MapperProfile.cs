using AutoMapper;
using Flashcards.Domain.Commands;
using Flashcards.Domain.Entities;

namespace Flashcards.Infrastructure.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserAddCommand, UserEntity>();
        }
    }
}
