using BusinessLayer.Dtos.Messages;
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
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost("SendMessage")]
        public async Task<ActionResult> SendMessage(SendMessageRequestDto requestDto)
        {
            var message = await _messageService.SendMessage(requestDto);
            return Ok(message);
        }

        [HttpGet("GetReceivedMessages")]
        public async Task<ActionResult> GetReceivedMessages()
        {
            var messages = await _messageService.GetReceivedMessages();
            return Ok(messages);
        }
    }
}
