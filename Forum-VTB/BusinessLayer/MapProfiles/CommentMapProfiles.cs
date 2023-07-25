using AutoMapper;
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
            CreateMap<AdvertComment, GetCommentResponceDto>()
                .ForMember(dto => dto.NickName, q => q.MapFrom(src => src.UserProfile.NickName))
                .ForMember(dto => dto.UserName, q => q.MapFrom(src => src.UserProfile.UserName))
                .ForMember(dto => dto.UserPhoto, q => q.MapFrom(src => src.UserProfile.Photo))
                .ReverseMap();
        }
    }
}
