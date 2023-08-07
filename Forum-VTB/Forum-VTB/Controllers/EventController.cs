using BusinessLayer.Dtos.Common;
using BusinessLayer.Dtos.Events;
using BusinessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forum_VTB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateEvent")]
        public async Task<ActionResult> CreateEvent(CreateEventDto requestDto)
        {
            Event forumEvent; 
            try
            {
                forumEvent = await _eventService.CreateEvent(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }
            return Ok(forumEvent);
        }
    }
}
