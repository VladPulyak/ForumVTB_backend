using AutoMapper;
using BusinessLayer.Dtos.Messages;
using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BusinessLayer.Services
{
    public class UserMessageService : IUserMessageService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<UserProfile> _userManager;
        private readonly IUserMessageRepository _userMessageRepository;

        public UserMessageService(IMapper mapper, IHttpContextAccessor contextAccessor, UserManager<UserProfile> userManager, IUserMessageRepository userMessageRepository)
        {
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _userMessageRepository = userMessageRepository;
        }

        public async Task<UserMessageResponceDto> SendMessage(SendMessageRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var receiver = await _userManager.FindByEmailAsync(requestDto.ReceiverEmail);
            var message = _mapper.Map<UserMessage>(requestDto);
            message.SenderId = user.Id;
            message.ReceiverId = receiver.Id;
            message.DateOfCreation = DateTime.Now;
            message.Id = Guid.NewGuid().ToString();
            var addedUserMessage = await _userMessageRepository.Add(message);
            await _userMessageRepository.Save();
            return new UserMessageResponceDto
            {
                DateOfCreation = addedUserMessage.DateOfCreation,
                ReceiverUserName = receiver.UserName,
                SenderUserName = user.UserName,
                Text = addedUserMessage.Text
            };
        }

        public async Task<List<UserMessageResponceDto>> GetReceivedMessages()
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var messages = await _userMessageRepository.GetByReceiverId(user.Id);
            var responceDtos = _mapper.Map<List<UserMessageResponceDto>>(messages);
            return responceDtos;
        }

        public async Task<List<UserMessageResponceDto>> GetSendedMessages()
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var messages = await _userMessageRepository.GetBySenderId(user.Id);
            var responceDtos = _mapper.Map<List<UserMessageResponceDto>>(messages);
            return responceDtos;
        }

        public async Task<UserMessageResponceDto> UpdateMessage(UpdateMessageRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var sender = await _userManager.FindByEmailAsync(userEmail);
            var userMessage = await _userMessageRepository.GetByDateOfCreation(requestDto.DateOfCreation, sender.Id);
            userMessage.Text = requestDto.Text;
            userMessage.DateOfCreation = DateTime.Now;
            var updatedUserMessage = _userMessageRepository.Update(userMessage);
            await _userMessageRepository.Save();
            return new UserMessageResponceDto
            {
                DateOfCreation = updatedUserMessage.DateOfCreation,
                ReceiverUserName = userMessage.Receiver.UserName,
                SenderUserName = sender.UserName,
                Text = updatedUserMessage.Text
            };
        }

        public async Task DeleteMessage(DeleteMessageRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var sender = await _userManager.FindByEmailAsync(userEmail);
            await _userMessageRepository.Delete(requestDto.DateOfCreation, sender.Id);
            await _userMessageRepository.Save();
        }
    }
}
