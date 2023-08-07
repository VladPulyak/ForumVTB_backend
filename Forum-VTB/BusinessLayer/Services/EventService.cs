using AutoMapper;
using BusinessLayer.Dtos.Events;
using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class EventService : IEventService
    {
        private readonly IRepository<Event> _eventRepository;
        private readonly IMapper _mapper;

        public EventService(IRepository<Event> eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<Event> CreateEvent(CreateEventDto requestDto)
        {
            var forumEvent = _mapper.Map<Event>(requestDto);
            forumEvent.Id = Guid.NewGuid().ToString();
            return forumEvent;
        }
    }
}
