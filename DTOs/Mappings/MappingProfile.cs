using AutoMapper;
using webapi.Models;

namespace musics_api.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Music, MusicDTO>().ReverseMap();
            CreateMap<Author, AuthorDTO>().ReverseMap();
        }
    }
}