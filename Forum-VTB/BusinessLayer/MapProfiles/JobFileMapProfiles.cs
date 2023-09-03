using AutoMapper;
using BusinessLayer.Dtos.AdvertFiles;
using BusinessLayer.Dtos.JobFiles;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.MapProfiles
{
    public class JobFileMapProfiles : Profile
    {
        public JobFileMapProfiles()
        {
            CreateMap<JobFile, GetJobFileResponceDto>()
                .ForMember(q => q.FileString, w => w.MapFrom(q => q.FileURL))
                .ReverseMap();
        }
    }
}
