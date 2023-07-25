using AutoMapper;
using BusinessLayer.Dtos.Messages;
using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<UserProfile> _userManager;
        private readonly IUserMessageRepository _userMessageRepository;

        public MessageService(IMapper mapper, IHttpContextAccessor contextAccessor, UserManager<UserProfile> userManager, IUserMessageRepository userMessageRepository)
        {
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _userMessageRepository = userMessageRepository;
        }

        public async Task<UserMessage> SendMessage(SendMessageRequestDto requestDto)
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
            return addedUserMessage;
        }

        public async Task<IEnumerable<UserMessage>> GetReceivedMessages()
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var messages = await _userMessageRepository.GetByReceiverId(user.Id);
            return messages;
        }

        public async Task<IEnumerable<UserMessage>> GetSendedMessages()
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var messages = await _userMessageRepository.GetBySenderId(user.Id);
            return messages;
        }
    }
}
