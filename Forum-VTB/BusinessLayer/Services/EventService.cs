using AutoMapper;
using BusinessLayer.Dtos.Common;
using BusinessLayer.Dtos.Events;
using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BusinessLayer.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<UserProfile> _userManager;
        private readonly ISubsectionRepository _subsectionRepository;

        public EventService(IEventRepository eventRepository, IMapper mapper, IHttpContextAccessor contextAccessor, UserManager<UserProfile> userManager, ISubsectionRepository subsectionRepository)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _subsectionRepository = subsectionRepository;
        }

        public async Task<CreateEventResponceDto> CreateEvent(CreateEventRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            //var date = Convert.ToDateTime(requestDto.StartDate);
            var @event = _mapper.Map<Event>(requestDto);
            var subsection = await _subsectionRepository.GetBySubsectionAndSectionNames(requestDto.SectionName, requestDto.SubsectionName);
            @event.Subsection = subsection;
            @event.SubsectionId = subsection.Id;
            @event.Id = Guid.NewGuid().ToString();
            var addedEvent = await _eventRepository.Add(@event);
            await _eventRepository.Save();
            return new CreateEventResponceDto
            {
                EventId = addedEvent.Id,
                Title = addedEvent.Title,
                Description = addedEvent.Description,
                Price = addedEvent.Price,
                StartDate = addedEvent.StartDate,
                EndDate = addedEvent.EndDate,
                Address = addedEvent.Address,
                PhoneNumber = addedEvent.PhoneNumber,
                Poster = addedEvent.Poster,
                SectionName = requestDto.SectionName,
                SubsectionName = requestDto.SubsectionName
            };
        }

        public async Task<UpdateEventResponceDto> UpdateEvent(UpdateEventRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var @event = await _eventRepository.GetById(requestDto.EventId);
            var subsection = await _subsectionRepository.GetBySubsectionAndSectionNames(requestDto.SectionName, requestDto.SubsectionName);
            @event.Title = requestDto.Title;
            @event.Description = requestDto.Description;
            @event.Price = requestDto.Price;
            @event.StartDate = requestDto.StartDate;
            @event.EndDate = requestDto.EndDate;
            @event.Subsection = subsection;
            @event.SubsectionId = subsection.Id;
            var updatedEvent = _eventRepository.Update(@event);
            await _eventRepository.Save();
            return new UpdateEventResponceDto
            {
                EventId = updatedEvent.Id,
                Title = updatedEvent.Title,
                Description = updatedEvent.Description,
                Price = updatedEvent.Price,
                StartDate = updatedEvent.StartDate,
                EndDate = updatedEvent.EndDate,
                Address = updatedEvent.Address,
                PhoneNumber = updatedEvent.PhoneNumber,
                Poster = updatedEvent.Poster,
                SectionName = requestDto.SectionName,
                SubsectionName = requestDto.SubsectionName
            };
        }

        public async Task<List<EventResponceDto>> GetFourNewestEvents()
        {
            var events = await _eventRepository.GetAll().OrderByDescending(q => q.StartDate).Take(4).ToListAsync();
            var responceDtos = _mapper.Map<List<EventResponceDto>>(events);
            return responceDtos;
        }

        public async Task DeleteEvent(DeleteEventRequestDto requestDto)
        {
            await _eventRepository.Delete(requestDto.EventId);
            await _eventRepository.Save();
        }

        public async Task<EventResponceDto> GetEventCard(GetEventCardRequestDto requestDto)
        {
            var @event = await _eventRepository.GetById(requestDto.EventId);

            return new EventResponceDto
            {
                EventId = @event.Id,
                Title = @event.Title,
                Description = @event.Description,
                Price = @event.Price,
                StartDate = @event.StartDate,
                EndDate = @event.EndDate,
                Address = @event.Address,
                PhoneNumber = @event.PhoneNumber,
                Poster = @event.Poster,
                SectionName = @event.Subsection.Section.Name,
                SubsectionName = @event.Subsection.Name
            };
        }

        public async Task<List<EventResponceDto>> FindBySubsectionName(FindBySubsectionNameRequestDto requestDto)
        {
            var events = await _eventRepository.GetBySubsectionName(requestDto.SubsectionName, requestDto.SectionName);
            var responceDtos = _mapper.Map<List<EventResponceDto>>(events.OrderByDescending(q => q.StartDate).ToList());
            return responceDtos;
        }

        public async Task<List<EventResponceDto>> GetByDate(GetByDateRequestDto requestDto)
        {
            var events = await _eventRepository.GetByDate(requestDto.Date);
            return _mapper.Map<List<EventResponceDto>>(events);
        }

        public async Task<List<EventResponceDto>> GetByDateInSubsection(GetByDateInSubsectionRequestDto requestDto)
        {
            var events = await _eventRepository.GetByDateInSubsection(requestDto.Date, requestDto.SubsectionName);
            return _mapper.Map<List<EventResponceDto>>(events);
        }
    }
}
