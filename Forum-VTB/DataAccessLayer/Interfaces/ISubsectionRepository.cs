using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface ISubsectionRepository : IRepository<Subsection>
    {
        Task<Subsection> GetByName(string name);

        Task<List<Subsection>> GetBySectionName(string sectionName);
    }
}
