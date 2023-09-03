using AutoMapper;
using BusinessLayer.Dtos.Account;
using BusinessLayer.Dtos.AdvertComments;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.MapProfiles
{
    public class CommentMapProfiles : Profile
    {
        public CommentMapProfiles()
        {
            CreateMap<AdvertComment, CreateAdvertCommentRequestDto>().ReverseMap();
            CreateMap<AdvertComment, ReplyAdvertCommentRequestDto>().ReverseMap();
            CreateMap<AdvertComment, ReplyAdvertCommentResponceDto>().ReverseMap();
            CreateMap<AdvertComment, Re_ReplyAdvertCommentDto>()
                .ForMember(dest => dest.NickName, q => q.MapFrom(src => src.UserProfile.NickName))
                .ForMember(dest => dest.UserName, q => q.MapFrom(src => src.UserProfile.UserName))
                .ForMember(dest => dest.UserPhoto, q => q.MapFrom(src => src.UserProfile.Photo))
                .ForMember(dest => dest.ParentCommentId, q => q.MapFrom(src => src.ParentCommentId))
                .ForMember(dest => dest.CommentId, q => q.MapFrom(src => src.Id))
                .ReverseMap();
            CreateMap<AdvertComment, GetAdvertCommentResponceDto>()
                .ForMember(dest => dest.NickName, q => q.MapFrom(src => src.UserProfile.NickName))
                .ForMember(dest => dest.UserName, q => q.MapFrom(src => src.UserProfile.UserName))
                .ForMember(dest => dest.UserPhoto, q => q.MapFrom(src => src.UserProfile.Photo))
                .ForMember(dest => dest.Replies, q => q.MapFrom(src => src.Replies))
                .ForMember(dest => dest.CommentId, q => q.MapFrom(src => src.Id))
                .ReverseMap();
        }
    }
}
