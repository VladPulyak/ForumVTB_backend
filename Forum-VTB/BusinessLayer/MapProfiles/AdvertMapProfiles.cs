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
            CreateMap<Advert, UpdateAdvertRequestDto>().ReverseMap();
            CreateMap<Advert, UserAdvertResponceDto>().ReverseMap();
        }
    }
}
