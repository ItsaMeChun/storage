using AutoMapper;
using hcode.DTO;
using hcode.Entity;

namespace hcode.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<Sauce, SauceDTO>().ReverseMap();
            CreateMap<SauceHistory, SauceHistoryDTO>().ReverseMap();
            CreateMap<SauceType, SauceTypeDTO>().ReverseMap();
            CreateMap<Types, TypesDTO>().ReverseMap();
        }
    }
}
