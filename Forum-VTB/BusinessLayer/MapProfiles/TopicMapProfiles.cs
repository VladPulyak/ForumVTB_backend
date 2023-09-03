using AutoMapper;
using BusinessLayer.Dtos.Advert;
using BusinessLayer.Dtos.Topic;
using DataAccessLayer.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.MapProfiles
{
    public class TopicMapProfiles : Profile
    {
        public TopicMapProfiles()
        {
            CreateMap<CreateTopicRequestDto, Topic>().ReverseMap();
            CreateMap<TopicResponceDto, Topic>().ReverseMap();
            CreateMap<Topic, TopicResponceDto>()
                .ForMember(dest => dest.TopicId, q => q.MapFrom(src => src.Id))
                .ForMember(dest => dest.MainPhoto, q => q.MapFrom(src => src.MainPhoto))
                .ForMember(dest => dest.UserName, q => q.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.NickName, q => q.MapFrom(src => src.User.NickName))
                .ForMember(dest => dest.UserPhoto, q => q.MapFrom(src => src.User.Photo))
                .ForMember(dest => dest.SectionName, q => q.MapFrom(src => src.Subsection.Section.Name))
                .ForMember(dest => dest.SubsectionName, q => q.MapFrom(src => src.Subsection.Name))
                .ForMember(dest => dest.MessagesCount, q => q.MapFrom(src => src.Messages.IsNullOrEmpty() ? 0 : src.Messages.Count))
                .ForMember(dest => dest.DateOfLastMessage, q => q.MapFrom(src => src.Messages.IsNullOrEmpty() ? default(DateTime) : src.Messages.Last().DateOfCreation));
            CreateMap<Topic, UpdateTopicRequestDto>().ReverseMap();
            CreateMap<Topic, UserTopicResponceDto>().ReverseMap();

        }
    }
}
