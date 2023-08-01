using BusinessLayer.Dtos.Messages;
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

        Task<List<UserMessageResponceDto>> GetReceivedMessages();

        Task<List<UserMessageResponceDto>> GetSendedMessages();

        Task<UserMessageResponceDto> UpdateMessage(UpdateMessageRequestDto requestDto);

        Task DeleteMessage(DeleteMessageRequestDto requestDto);
    }
}
