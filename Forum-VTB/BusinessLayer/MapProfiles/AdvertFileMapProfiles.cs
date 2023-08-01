using AutoMapper;
using BusinessLayer.Dtos.AdvertFiles;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.MapProfiles
{
    public class AdvertFileMapProfiles : Profile
    {
        public AdvertFileMapProfiles()
        {
            //CreateMap<AdvertFile, AddAdvertFileRequestDto>().ReverseMap();
            CreateMap<AdvertFile, GetAdvertFileResponceDto>()
                .ForMember(q => q.FileString, w => w.MapFrom(q => q.FileURL))
                .ReverseMap();
        }
    }
}
