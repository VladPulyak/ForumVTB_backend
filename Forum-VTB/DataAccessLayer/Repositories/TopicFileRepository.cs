using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class TopicFileRepository : Repository<TopicFile>, ITopicFileRepository
    {
        public TopicFileRepository(ForumVTBDbContext context) : base(context)
        {
            
        }
    }
}
