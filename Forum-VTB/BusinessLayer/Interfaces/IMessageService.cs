using BusinessLayer.Dtos.Messages;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IMessageService
    {
        Task<UserMessage> SendMessage(SendMessageRequestDto requestDto);

        Task<IEnumerable<UserMessage>> GetReceivedMessages();
    }
}
