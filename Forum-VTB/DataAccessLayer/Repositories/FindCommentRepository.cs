using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class FindCommentRepository : Repository<FindComment>
    {
        public FindCommentRepository(ForumVTBDbContext context) : base(context)
        {
            
        }
    }
}
