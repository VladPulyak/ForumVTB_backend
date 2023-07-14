using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class MessageRepository : Repository<TopicMessage>
    {
        public MessageRepository(ForumVTBDbContext context) : base(context)
        {
        }
    }
}
