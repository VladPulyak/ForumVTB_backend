using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class TopicRepository : Repository<Advert>
    {
        public TopicRepository(ForumVTBDbContext context) : base(context)
        {
        }
    }
}
