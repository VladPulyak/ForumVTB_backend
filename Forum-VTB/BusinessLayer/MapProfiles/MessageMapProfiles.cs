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
    internal class MessageMapProfiles : Profile
    {
        public MessageMapProfiles()
        {
            CreateMap<SupportMessageRequestDto, TopicMessage>()
                .ForMember("Text", q => q.MapFrom(w => w.Text));
            CreateMap<SendMessageRequestDto, UserMessage>().ReverseMap();
        }
    }
}
