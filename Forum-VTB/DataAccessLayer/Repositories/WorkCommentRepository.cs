using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class WorkCommentRepository : Repository<WorkComment>
    {
        public WorkCommentRepository(ForumVTBDbContext context) : base(context)
        {

        }
    }
}
