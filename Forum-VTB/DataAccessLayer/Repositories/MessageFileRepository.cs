using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class MessageFileRepository : Repository<MessageFile>
    {
        public MessageFileRepository(ForumVTBDbContext context) : base(context)
        {
        }
    }
}
