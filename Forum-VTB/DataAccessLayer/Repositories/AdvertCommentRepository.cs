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
    public class AdvertCommentRepository : Repository<AdvertComment>, IAdvertCommentRepository
    {
        public AdvertCommentRepository(ForumVTBDbContext context) : base(context)
        {
        }

        public async Task<AdvertComment> GetById(string id)
        {
            var comment = await _set.Where(q => q.Id == id).Include(q => q.Advert).Include(q => q.UserProfile).SingleAsync();

            if (comment is null)
            {
                throw new ObjectNotFoundException("Comment not found");
            }

            return comment;
        }

        public async Task Delete(string id)
        {
            var comment = await GetById(id);
            _set.Remove(comment);
        }

        public async Task<List<AdvertComment>> GetByAdvertId(string advertId)
        {
            var comments = await _set.Where(q => q.AdvertId == advertId && q.ParentCommentId == null)
                                     .Include(q => q.UserProfile)
                                     .Include(q => q.Replies)
                                     .ToListAsync();

            return comments;
        }

        public async Task<AdvertComment> GetByDateOfCreation(DateTime dateOfCreation, string userId)
        {
            var advertComment = await _set.Where(q => q.DateOfCreation == dateOfCreation && q.UserId == userId).Include(q => q.UserProfile).SingleOrDefaultAsync();

            if (advertComment is null)
            {
                throw new ObjectNotFoundException("Comment with this date of creation or from this user is not found");
            }

            return advertComment;
        }

    }
}
