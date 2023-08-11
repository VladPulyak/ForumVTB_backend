using AutoMapper;
using BusinessLayer.Dtos.Account;
using BusinessLayer.Dtos.Authentication;
using BusinessLayer.Dtos.UserProfiles;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.MapProfiles
{
    internal class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<UserProfile, UserRegisterDto>().ReverseMap();
            CreateMap<UserProfile, UserProfileInfoResponceDto>()
                .ForMember(dest => dest.UserId, q => q.MapFrom(src => src.Id))
                .ReverseMap();
            CreateMap<UserProfile, GetUserProfileInfoResponceDto>()
                .ForMember(dest => dest.Username, q => q.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Photo, q => q.MapFrom(src => src.Photo))
                .ForMember(dest => dest.NickName, q => q.MapFrom(src => src.NickName))
                .ReverseMap();
        }
    }
}
