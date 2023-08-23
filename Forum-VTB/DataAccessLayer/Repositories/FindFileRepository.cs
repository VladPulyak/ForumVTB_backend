using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class FindFileRepository : Repository<FindFile>
    {
        public FindFileRepository(ForumVTBDbContext context) : base(context)
        {
            
        }
    }
}
