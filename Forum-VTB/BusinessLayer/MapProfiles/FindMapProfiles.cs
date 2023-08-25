using AutoMapper;
using BusinessLayer.Dtos.Advert;
using BusinessLayer.Dtos.Find;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.MapProfiles
{
    public class FindMapProfiles : Profile
    {
        public FindMapProfiles()
        {
            CreateMap<CreateFindRequestDto, Find>().ReverseMap();
            CreateMap<FindResponceDto, Find>().ReverseMap();
            CreateMap<Find, FindResponceDto>()
                .ForMember(dest => dest.FindId, q => q.MapFrom(src => src.Id))
                .ForMember(dest => dest.MainPhoto, q => q.MapFrom(src => src.MainPhoto))
                .ForMember(dest => dest.SectionName, q => q.MapFrom(src => src.Subsection.Section.Name))
                .ForMember(dest => dest.SubsectionName, q => q.MapFrom(src => src.Subsection.Name));
            CreateMap<Find, UpdateFindRequestDto>().ReverseMap();
            CreateMap<Find, UserFindResponceDto>().ReverseMap();
        }
    }
}
