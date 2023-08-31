using AutoMapper;
using BusinessLayer.Dtos.AdvertFiles;
using BusinessLayer.Dtos.TopicFile;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.MapProfiles
{
    public class TopicFileMapProfiles : Profile
    {
        public TopicFileMapProfiles()
        {
            CreateMap<TopicFile, GetTopicFileResponceDto>()
                .ForMember(q => q.FileString, w => w.MapFrom(q => q.FileURL))
                .ReverseMap();
        }
    }
}
