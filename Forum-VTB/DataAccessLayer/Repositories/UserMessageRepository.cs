using DataAccessLayer.Exceptions;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class UserMessageRepository : Repository<UserMessage>, IUserMessageRepository
    {
        public UserMessageRepository(ForumVTBDbContext context) : base(context)
        {
        }

        public async Task<List<UserMessage>> GetByReceiverId(string id)
        {
            var userMessage = await _set.Where(q => q.ReceiverId == id).ToListAsync();

            if (userMessage is null)
            {
                throw new ObjectNotFoundException("Received messages not found");
            }

            return userMessage;

        }

        public async Task<List<UserMessage>> GetBySenderId(string id)
        {
            var userMessage = await _set.Where(q => q.SenderId == id).ToListAsync();

            if (userMessage is null)
            {
                throw new ObjectNotFoundException("Sended messages not found");
            }

            return userMessage;

        }
    }
}
