using AutoMapper;
using BusinessLayer.Dtos.AdvertFiles;
using BusinessLayer.Dtos.FindFIles;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.MapProfiles
{
    public class FindFileMapProfiles : Profile
    {
        public FindFileMapProfiles()
        {
            CreateMap<FindFile, GetFindFileResponceDto>()
                .ForMember(q => q.FileString, w => w.MapFrom(q => q.FileURL))
                .ReverseMap();
        }
    }
}
