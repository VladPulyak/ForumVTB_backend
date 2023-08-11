using AutoMapper;
using BusinessLayer.Dtos.Account;
using BusinessLayer.Dtos.Messages;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.MapProfiles
{
    internal class UserMessageMapProfiles : Profile
    {
        public UserMessageMapProfiles()
        {
            CreateMap<SendMessageRequestDto, UserMessage>().ReverseMap();
            CreateMap<UserMessage, UserMessageResponceDto>()
                .ForMember(q => q.ReceiverUserName, w => w.MapFrom(q => q.Receiver.UserName))
                .ForMember(q => q.SenderUserName, w => w.MapFrom(q => q.Sender.UserName))
                .ReverseMap();
            CreateMap<UserMessage, GetChatMessageResponceDto>()
                .ForMember(q => q.SenderId, w => w.MapFrom(q => q.SenderId))
                .ReverseMap();
        }
    }
}
