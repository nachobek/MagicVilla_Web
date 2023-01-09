using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;

namespace MagicVilla_Web.Utility
{
    class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<VillaReadDTO, VillaCreateDTO>().ReverseMap();

            CreateMap<VillaReadDTO, VillaUpdateDTO>().ReverseMap();

            CreateMap<VillaNumberReadDTO, VillaNumberCreateDTO>().ReverseMap();

            CreateMap<VillaNumberReadDTO, VillaNumberUpdateDTO>().ReverseMap();
        }
    }
}