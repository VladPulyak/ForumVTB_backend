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
    public class AdvertRepository : Repository<Advert>, IAdvertRepository
    {
        public AdvertRepository(ForumVTBDbContext context) : base(context)
        {

        }

        //public async Task Delete(DateTime dateOfCreation, string userId)
        //{
        //    var entity = await GetByDateOfCreationForUser(dateOfCreation, userId);
        //    _set.Remove(entity);
        //}

        public async Task Delete(string advertId)
        {
            var entity = await GetById(advertId);
            _set.Remove(entity);
        }

        public async Task<List<Advert>> GetByUserId(string userId)
        {
            var adverts = await _set.Where(q => q.UserId == userId).Include(q => q.Comments).ToListAsync();
            if (!adverts.Any())
            {
                throw new ObjectNotFoundException("Adverts for this user is not found");
            }

            return adverts;
        }

        //public async Task<Advert> GetByDateOfCreationForUser(DateTime dateOfCreation, string userId)
        //{
        //    var userAdverts = await GetByUserId(userId);
        //    var advert = userAdverts.SingleOrDefault(q => q.DateOfCreation == dateOfCreation);
        //    if (advert is null)
        //    {
        //        throw new ObjectNotFoundException("Advert with this date is not found");
        //    }

        //    return advert;
        //}

        public async Task<Advert> GetById(string advertId)
        {
            var advert = await _set.Where(q => q.Id == advertId).Include(q => q.Comments).Include(q => q.Subsection).SingleAsync();
            if (advert is null)
            {
                throw new ObjectNotFoundException("Adverts with this id is not found");
            }

            return advert;

        }
    }
}
