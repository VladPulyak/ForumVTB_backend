using BusinessLayer.Dtos.Account;
using BusinessLayer.Dtos.Common;
using BusinessLayer.Dtos.Messages;
using BusinessLayer.Dtos.UserProfiles;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forum_VTB.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IUserMessageService _messageService;

        public MessageController(IUserMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost("SendMessage")]
        public async Task<ActionResult> SendMessage(SendMessageRequestDto requestDto)
        {
            UserMessageResponceDto responceDto;

            try
            {
                responceDto = await _messageService.SendMessage(requestDto);
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

        [HttpGet("GetReceivedMessages")]
        public async Task<ActionResult> GetReceivedMessages()
        {
            List<UserMessageResponceDto> responceDtos;

            try
            {
                responceDtos = await _messageService.GetReceivedMessages();
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

        [HttpGet("GetSendedMessages")]
        public async Task<ActionResult> GetSendedMessages()
        {
            List<UserMessageResponceDto> responceDtos;

            try
            {
                responceDtos = await _messageService.GetSendedMessages();
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

        [HttpPut("UpdateMessage")]
        public async Task<ActionResult> UpdateMessage(UpdateMessageRequestDto requestDto)
        {
            UserMessageResponceDto responceDto;

            try
            {
                responceDto = await _messageService.UpdateMessage(requestDto);
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

        [HttpDelete("DeleteMessage")]
        public async Task<ActionResult> DeleteMessage(DeleteMessageRequestDto requestDto)
        {
            try
            {
                await _messageService.DeleteMessage(requestDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResponceDto
                {
                    Message = ex.Message
                });
            }
            return Ok("Message delete successfully!");
        }

        [HttpGet("GetChats")]
        public async Task<ActionResult> GetChats()
        {
            List<GetUserProfileInfoResponceDto> responceDtos;
            try
            {
                responceDtos = await _messageService.GetChats();
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
