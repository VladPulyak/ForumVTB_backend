using BusinessLayer.Dtos.Account;
using BusinessLayer.Dtos.Messages;
using BusinessLayer.Dtos.UserChat;
using BusinessLayer.Dtos.UserProfiles;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IUserMessageService
    {
        Task<UserMessageResponceDto> SendMessage(SendMessageRequestDto requestDto);

        Task<List<UserChatResponceDto>> GetChats();

        Task<List<GetChatMessageResponceDto>> GetChatMessages(string chatId);

        Task<UserChatResponceDto> CreateChat(CreateChatRequestDto requestDto);
    }
}
