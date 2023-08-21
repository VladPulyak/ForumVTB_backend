using AutoMapper;
using BusinessLayer.Dtos.Advert;
using BusinessLayer.Dtos.Job;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.MapProfiles
{
    public class JobMapProfiles : Profile
    {
        public JobMapProfiles()
        {
            CreateMap<CreateJobRequestDto, Job>().ReverseMap();
            CreateMap<JobResponceDto, Job>().ReverseMap();
            CreateMap<Job, JobResponceDto>()
                .ForMember(dest => dest.JobId, q => q.MapFrom(src => src.Id))
                .ForMember(dest => dest.MainPhoto, q => q.MapFrom(src => src.MainPhoto))
                .ForMember(dest => dest.SectionName, q => q.MapFrom(src => src.Subsection.Section.Name))
                .ForMember(dest => dest.SubsectionName, q => q.MapFrom(src => src.Subsection.Name));
            CreateMap<Job, UpdateJobRequestDto>().ReverseMap();
            CreateMap<Job, UserJobResponceDto>().ReverseMap();
        }
    }
}
