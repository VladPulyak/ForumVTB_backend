using AutoMapper;
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
            CreateMap<Event, CreateEventDto>().ReverseMap();
        }
    }
}
