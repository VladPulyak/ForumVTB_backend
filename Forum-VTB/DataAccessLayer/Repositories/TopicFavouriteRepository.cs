using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class TopicFavouriteRepository : Repository<TopicFavourite>, ITopicFavouriteRepository
    {
        public TopicFavouriteRepository(ForumVTBDbContext context) : base(context)
        {

        }
    }
}
