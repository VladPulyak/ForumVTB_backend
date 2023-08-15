using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class WorkFavouriteRepository : Repository<WorkFavourite>
    {
        public WorkFavouriteRepository(ForumVTBDbContext context) : base(context)
        {

        }
    }
}
