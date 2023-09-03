using AutoMapper;
using BusinessLayer.Dtos.Advert;
using BusinessLayer.Dtos.Events;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.MapProfiles
{
    public class EventMapProfiles : Profile
    {
        public EventMapProfiles()
        {
            CreateMap<Event, CreateEventRequestDto>().ReverseMap();
            CreateMap<Event, EventResponceDto>()
                .ForMember(dest => dest.EventId, q => q.MapFrom(src => src.Id))
                .ForMember(dest => dest.SectionName, q => q.MapFrom(src => src.Subsection.Section.Name))
                .ForMember(dest => dest.SubsectionName, q => q.MapFrom(src => src.Subsection.Name));

        }
    }
}
