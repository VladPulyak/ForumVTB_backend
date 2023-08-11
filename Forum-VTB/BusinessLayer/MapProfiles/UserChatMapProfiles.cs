using AutoMapper;
using BusinessLayer.Dtos.UserChat;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.MapProfiles
{
    public class UserChatMapProfiles : Profile
    {
        public UserChatMapProfiles()
        {
            CreateMap<UserChat, UserChatResponceDto>()
                .ForMember(dest => dest.ChatId, q => q.MapFrom(src => src.Id))
                .ReverseMap();
        }
    }
}
