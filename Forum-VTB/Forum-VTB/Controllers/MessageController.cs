using BusinessLayer.Dtos.Account;
using BusinessLayer.Dtos.Common;
using BusinessLayer.Dtos.Messages;
using BusinessLayer.Dtos.UserChat;
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

        [HttpGet("GetChats")]
        public async Task<ActionResult> GetChats()
        {
            List<UserChatResponceDto> responceDtos;
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

        [HttpGet("GetChatMessages")]
        public async Task<ActionResult> GetChatMessages(string chatId)
        {
            List<GetChatMessageResponceDto> responceDtos;
            try
            {
                responceDtos = await _messageService.GetChatMessages(chatId);
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

        [HttpPost("CreateChat")]
        public async Task<ActionResult> CreateChat(CreateChatRequestDto requestDto)
        {
            UserChatResponceDto responceDto;
            try
            {
                responceDto = await _messageService.CreateChat(requestDto);
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
    }
}
