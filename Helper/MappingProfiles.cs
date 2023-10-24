using AutoMapper;
using hcode.DTO;
using hcode.Entity;

namespace hcode.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Author, AuthorDTO>().ReverseMap();
        }
    }
}
