using BusinessLayer.Dtos.Common;
using BusinessLayer.Dtos.Events;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IEventService
    {
        Task<CreateEventResponceDto> CreateEvent(CreateEventRequestDto requestDto);

        Task<UpdateEventResponceDto> UpdateEvent(UpdateEventRequestDto requestDto);

        Task<List<EventResponceDto>> GetFourNewestEvents();

        Task DeleteEvent(DeleteEventRequestDto requestDto);

        Task<EventResponceDto> GetEventCard(GetEventCardRequestDto requestDto);

        Task<List<EventResponceDto>> FindBySubsectionName(FindBySubsectionNameRequestDto requestDto);

        Task<List<EventResponceDto>> GetByDate(GetByDateRequestDto requestDto);

        Task<List<EventResponceDto>> GetByDateInSubsection(GetByDateInSubsectionRequestDto requestDto);
    }
}
