using AutoMapper;
using BusinessLayer.Dtos.AdvertComments;
using BusinessLayer.Dtos.TopicMessage;
using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class TopicMessageService : ITopicMessageService
    {
        private readonly ITopicMessageRepository _topicMessageRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<UserProfile> _userManager;

        public TopicMessageService(ITopicMessageRepository topicMessageRepository, IMapper mapper, IHttpContextAccessor contextAccessor, UserManager<UserProfile> userManager)
        {
            _topicMessageRepository = topicMessageRepository;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public async Task<CreateTopicMessageResponceDto> CreateTopicMessage(CreateTopicMessageRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var message = _mapper.Map<TopicMessage>(requestDto);
            message.UserId = user.Id;
            message.Id = Guid.NewGuid().ToString();
            message.TopicId = requestDto.TopicId;
            message.DateOfCreation = DateTime.UtcNow;
            var addedMessage = await _topicMessageRepository.Add(message);
            await _topicMessageRepository.Save();
            return new CreateTopicMessageResponceDto
            {
                MessageId = addedMessage.Id,
                TopicId = addedMessage.TopicId,
                DateOfCreation = addedMessage.DateOfCreation,
                NickName = user.NickName,
                UserName = user.UserName,
                Text = addedMessage.Text
            };
        }

        public async Task<UpdateTopicMessageResponceDto> UpdateTopicMessage(UpdateTopicMessageRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var message = await _topicMessageRepository.GetById(requestDto.MessageId);
            message.Text = requestDto.Text;
            message.DateOfCreation = DateTime.UtcNow;
            var updatedMessage = _topicMessageRepository.Update(message);
            await _topicMessageRepository.Save();
            return new UpdateTopicMessageResponceDto
            {
                MessageId = updatedMessage.Id,
                TopicId = updatedMessage.TopicId,
                DateOfCreation = updatedMessage.DateOfCreation,
                NickName = user.NickName,
                UserName = user.UserName,
                Text = updatedMessage.Text
            };
        }

        public async Task DeleteTopicMessage(DeleteTopicMessageRequestDto requestDto)
        {
            await _topicMessageRepository.Delete(requestDto.MessageId);
            await _topicMessageRepository.Save();
        }

        public async Task<List<GetTopicMessageResponceDto>> GetTopicMessagesByTopicId(GetTopicMessageRequestDto requestDto)
        {
            var messages = await _topicMessageRepository.GetByTopicId(requestDto.TopicId);
            var responceDtos = _mapper.Map<List<GetTopicMessageResponceDto>>(messages.OrderBy(q => q.DateOfCreation));
            return responceDtos;
        }

        public async Task<ReplyTopicMessageResponceDto> ReplyTopicMessage(ReplyTopicMessageRequestDto requestDto)
        {
            var userEmail = _contextAccessor.HttpContext?.User.Claims.Single(q => q.Type == ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            var parentMessage = await _topicMessageRepository.GetById(requestDto.ParentMessageId);
            var message = _mapper.Map<TopicMessage>(requestDto);
            message.UserId = user.Id;
            message.Id = Guid.NewGuid().ToString();
            message.ParentMessageId = parentMessage.Id;
            message.ParentMessage = parentMessage;
            message.DateOfCreation = DateTime.UtcNow;
            var addedComment = await _topicMessageRepository.Add(message);
            await _topicMessageRepository.Save();
            return new ReplyTopicMessageResponceDto
            {
                MessageId = addedComment.Id,
                TopicId = addedComment.TopicId,
                DateOfCreation = addedComment.DateOfCreation,
                NickName = user.NickName,
                UserName = user.UserName,
                Text = addedComment.Text
            };
        }
    }
}