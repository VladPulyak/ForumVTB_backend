using AutoMapper;
using BusinessLayer.Dtos.AdvertComments;
using BusinessLayer.Dtos.FindComments;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.MapProfiles
{
    public class FindCommentMapProfiles : Profile
    {
        public FindCommentMapProfiles()
        {
            CreateMap<FindComment, CreateFindCommentRequestDto>().ReverseMap();
            CreateMap<FindComment, ReplyFindCommentRequestDto>().ReverseMap();
            CreateMap<FindComment, ReplyFindCommentResponceDto>().ReverseMap();
            CreateMap<FindComment, Re_ReplyFindCommentDto>()
                .ForMember(dest => dest.NickName, q => q.MapFrom(src => src.UserProfile.NickName))
                .ForMember(dest => dest.UserName, q => q.MapFrom(src => src.UserProfile.UserName))
                .ForMember(dest => dest.UserPhoto, q => q.MapFrom(src => src.UserProfile.Photo))
                .ForMember(dest => dest.ParentCommentId, q => q.MapFrom(src => src.ParentCommentId))
                .ForMember(dest => dest.CommentId, q => q.MapFrom(src => src.Id))
                .ReverseMap();
            CreateMap<FindComment, GetFindCommentResponceDto>()
                .ForMember(dest => dest.NickName, q => q.MapFrom(src => src.UserProfile.NickName))
                .ForMember(dest => dest.UserName, q => q.MapFrom(src => src.UserProfile.UserName))
                .ForMember(dest => dest.UserPhoto, q => q.MapFrom(src => src.UserProfile.Photo))
                .ForMember(dest => dest.Replies, q => q.MapFrom(src => src.Replies))
                .ForMember(dest => dest.CommentId, q => q.MapFrom(src => src.Id))
                .ReverseMap();
        }
    }
}
