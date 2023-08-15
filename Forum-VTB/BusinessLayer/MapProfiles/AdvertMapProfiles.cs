using AutoMapper;
using BusinessLayer.Dtos.Advert;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.MapProfiles
{
    public class AdvertMapProfiles : Profile
    {
        public AdvertMapProfiles()
        {
            CreateMap<CreateAdvertRequestDto, Advert>().ReverseMap();
            CreateMap<AdvertResponceDto, Advert>().ReverseMap();
            CreateMap<Advert, AdvertResponceDto>()
                .ForMember(dest => dest.AdvertId, q => q.MapFrom(src => src.Id))
                .ForMember(dest => dest.MainPhoto, q => q.MapFrom(src => src.MainPhoto));
            CreateMap<Advert, UpdateAdvertRequestDto>().ReverseMap();
            CreateMap<Advert, UserAdvertResponceDto>().ReverseMap();
        }
    }
}
