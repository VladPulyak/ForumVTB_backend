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
    public class FindCommentRepository : Repository<FindComment>, IFindCommentRepository
    {
        public FindCommentRepository(ForumVTBDbContext context) : base(context)
        {
            
        }

        public async Task<FindComment> GetById(string id)
        {
            var comment = await _set.Where(q => q.Id == id).Include(q => q.Find).Include(q => q.UserProfile).SingleAsync();

            if (comment is null)
            {
                throw new ObjectNotFoundException("Comment not found");
            }

            return comment;
        }

        public async Task Delete(string commentId)
        {
            var comment = await GetById(commentId);
            _set.Remove(comment);
        }

        public async Task<List<FindComment>> GetByFindId(string findId)
        {
            var comments = await _set.Where(q => q.FindId == findId && q.ParentCommentId == null)
                                     .Include(q => q.UserProfile)
                                     .Include(q => q.Replies)
                                     .ToListAsync();

            return comments;
        }

    }
}
