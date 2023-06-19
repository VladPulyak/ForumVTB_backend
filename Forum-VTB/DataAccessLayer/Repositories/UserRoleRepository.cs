using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class UserRoleRepository : Repository<UserRole>
    {
        public UserRoleRepository(ForumVTBDbContext context) : base(context)
        {            
        }
    }
}
