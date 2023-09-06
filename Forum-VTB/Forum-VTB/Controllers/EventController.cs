using BusinessLayer.Dtos.Advert;
using BusinessLayer.Dtos.AdvertFiles;
using BusinessLayer.Dtos.Common;
using BusinessLayer.Dtos.Events;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forum_VTB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost("/Events/CreateEvent")]
        public async Task<ActionResult> CreateEvent(CreateEventRequestDto requestDto)
        {
            CreateEventResponceDto responceDto; 
            try
            {
                responceDto = await _eventService.CreateEvent(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }
            return Ok(responceDto);
        }

        [AllowAnonymous]
        [HttpGet("/Events/GetFourNewestEvents")]
        public async Task<ActionResult> GetFourNewestEvents()
        {
            List<EventResponceDto> responceDtos;
            try
            {
                responceDtos = await _eventService.GetFourNewestEvents();
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }

            return Ok(responceDtos);
        }


        [HttpPut("/Events/UpdateEvent")]
        public async Task<ActionResult> UpdateEvent(UpdateEventRequestDto requestDto)
        {
            UpdateEventResponceDto responceDto;
            try
            {
                responceDto = await _eventService.UpdateEvent(requestDto);

            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }

            return Ok(responceDto);
        }

        [HttpDelete("/Events/DeleteEvent")]
        public async Task<ActionResult> DeleteEvent(DeleteEventRequestDto requestDto)
        {
            try
            {
                await _eventService.DeleteEvent(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }

            return Ok("Event delete successfully!");
        }

        [AllowAnonymous]
        [HttpPost("/Events/GetEventCard")]
        public async Task<ActionResult> GetEventCard(GetEventCardRequestDto requestDto)
        {
            EventResponceDto responceDto;
            try
            {
                responceDto = await _eventService.GetEventCard(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }

            return Ok(responceDto);
        }

        [AllowAnonymous]
        [HttpPost("/Events/FindBySubsectionName")]
        public async Task<ActionResult> FindBySubsectionName(FindBySubsectionNameRequestDto requestDto)
        {
            List<EventResponceDto> responceDtos;

            try
            {
                responceDtos = await _eventService.FindBySubsectionName(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }
            return Ok(responceDtos);
        }

        [AllowAnonymous]
        [HttpPost("/Events/GetByDate")]
        public async Task<ActionResult> GetByDate(GetByDateRequestDto requestDto)
        {
            List<EventResponceDto> responceDtos;

            try
            {
                responceDtos = await _eventService.GetByDate(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }
            return Ok(responceDtos);
        }

        [AllowAnonymous]
        [HttpPost("/Events/GetByDateInSubsection")]
        public async Task<ActionResult> GetByDateInSubsection(GetByDateInSubsectionRequestDto requestDto)
        {
            List<EventResponceDto> responceDtos;

            try
            {
                responceDtos = await _eventService.GetByDateInSubsection(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }
            return Ok(responceDtos);
        }
    }
}
