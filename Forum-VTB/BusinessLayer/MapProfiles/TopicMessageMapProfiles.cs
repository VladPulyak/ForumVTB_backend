using AutoMapper;
using BusinessLayer.Dtos.FindComments;
using BusinessLayer.Dtos.TopicMessage;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.MapProfiles
{
    public class TopicMessageMapProfiles : Profile
    {
        public TopicMessageMapProfiles()
        {
            CreateMap<TopicMessage, CreateTopicMessageRequestDto>().ReverseMap();
            CreateMap<TopicMessage, ReplyTopicMessageRequestDto>().ReverseMap();
            CreateMap<TopicMessage, ReplyTopicMessageResponceDto>().ReverseMap();
            CreateMap<TopicMessage, Re_ReplyTopicMessageDto>()
                .ForMember(dest => dest.NickName, q => q.MapFrom(src => src.UserProfile.NickName))
                .ForMember(dest => dest.UserName, q => q.MapFrom(src => src.UserProfile.UserName))
                .ForMember(dest => dest.UserPhoto, q => q.MapFrom(src => src.UserProfile.Photo))
                .ForMember(dest => dest.ParentMessageId, q => q.MapFrom(src => src.ParentMessageId))
                .ForMember(dest => dest.MessageId, q => q.MapFrom(src => src.Id))
                .ReverseMap();
            CreateMap<TopicMessage, GetTopicMessageResponceDto>()
                .ForMember(dest => dest.NickName, q => q.MapFrom(src => src.UserProfile.NickName))
                .ForMember(dest => dest.UserName, q => q.MapFrom(src => src.UserProfile.UserName))
                .ForMember(dest => dest.UserPhoto, q => q.MapFrom(src => src.UserProfile.Photo))
                .ForMember(dest => dest.Replies, q => q.MapFrom(src => src.Replies))
                .ForMember(dest => dest.MessageId, q => q.MapFrom(src => src.Id))
                .ReverseMap();
        }
    }
}
