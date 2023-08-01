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
            var userMessage = await _set.Where(q => q.ReceiverId == id).Include(q => q.Sender).ToListAsync();

            if (userMessage is null)
            {
                throw new ObjectNotFoundException("Received messages not found");
            }

            return userMessage;
        }

        public async Task<List<UserMessage>> GetBySenderId(string id)
        {
            var userMessage = await _set.Where(q => q.SenderId == id).Include(q => q.Receiver).ToListAsync();

            if (userMessage is null)
            {
                throw new ObjectNotFoundException("Sended messages not found");
            }

            return userMessage;
        }

        public async Task<UserMessage> GetByDateOfCreation(DateTime dateOfCreation, string userId)
        {
            var userMessage = await _set.Where(q => q.DateOfCreation == dateOfCreation && q.SenderId == userId).Include(q => q.Receiver).SingleOrDefaultAsync();

            if (userMessage is null)
            {
                throw new ObjectNotFoundException("Message with this date of creation is not found");
            }

            return userMessage;
        }

        public async Task Delete(DateTime dateOfCreation, string userId)
        {
            var userMessage = await GetByDateOfCreation(dateOfCreation, userId);
            _set.Remove(userMessage);
        }
    }
}
