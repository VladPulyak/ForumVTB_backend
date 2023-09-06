using DataAccessLayer.Exceptions;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(ForumVTBDbContext context) : base(context)
        {

        }

        public override IQueryable<Event> GetAll()
        {
            return _set
                .Include(q => q.Subsection)
                .Include(q => q.Subsection.Section)
                .AsNoTracking();
        }

        public async Task Delete(string eventId)
        {
            var entity = await GetById(eventId);
            _set.Remove(entity);
        }

        public async Task<Event> GetById(string eventId)
        {
            var @event = await _set.Where(q => q.Id == eventId)
                .Include(q => q.Subsection)
                .Include(q => q.Subsection.Section)
                .SingleAsync();
            if (@event is null)
            {
                throw new ObjectNotFoundException("Event with this id is not found");
            }

            return @event;

        }

        public async Task<List<Event>> GetBySubsectionName(string subsectionName, string sectionName)
        {
            var events = await _set
                .Include(q => q.Subsection)
                .Include(q => q.Subsection.Section)
                .Where(q => q.Subsection.Name == subsectionName && q.Subsection.Section.Name == sectionName)
                .ToListAsync();

            return events;
        }

        public async Task<List<Event>> SearchByKeyPhrase(string keyPhrase)
        {
            keyPhrase = keyPhrase.Trim();
            return await _set.Where(a => a.Title.ToUpper().Contains(keyPhrase.ToUpper()) || a.Description.ToUpper().Contains(keyPhrase.ToUpper()))
                .Include(q => q.Subsection)
                .Include(q => q.Subsection.Section)
                .ToListAsync();
        }

        public async Task<List<Event>> GetByDate(DateTime date)
        {
            return await _set.Where(q => date > q.StartDate && date < q.EndDate).ToListAsync();
        }

        public async Task<List<Event>> GetByDateInSubsection(DateTime date, string subsectionName)
        {
            return await _set
                .Include(q => q.Subsection)
                .Where(q => (date > q.StartDate && date < q.EndDate) && q.Subsection.Name == subsectionName).ToListAsync();
        }
    }
}
