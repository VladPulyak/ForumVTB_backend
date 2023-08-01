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
            CreateMap<AdvertComment, CreateCommentRequestDto>().ReverseMap();
            CreateMap<AdvertComment, ReplyCommentRequestDto>().ReverseMap();
            CreateMap<AdvertComment, ReplyCommentResponceDto>().ReverseMap();
            CreateMap<AdvertComment, Re_ReplyCommentDto>()
                .ForMember(dto => dto.NickName, q => q.MapFrom(src => src.UserProfile.NickName))
                .ForMember(dto => dto.UserName, q => q.MapFrom(src => src.UserProfile.UserName))
                .ForMember(dto => dto.UserPhoto, q => q.MapFrom(src => src.UserProfile.Photo))
                .ForMember(dto => dto.ParentCommentId, q => q.MapFrom(src => src.ParentCommentId))
                .ReverseMap();
            CreateMap<AdvertComment, GetCommentResponceDto>()
                .ForMember(dto => dto.NickName, q => q.MapFrom(src => src.UserProfile.NickName))
                .ForMember(dto => dto.UserName, q => q.MapFrom(src => src.UserProfile.UserName))
                .ForMember(dto => dto.UserPhoto, q => q.MapFrom(src => src.UserProfile.Photo))
                .ForMember(dto => dto.Replies, q => q.MapFrom(src => src.Replies))
                .ReverseMap();
        }
    }
}
