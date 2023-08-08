﻿using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IAdvertCommentRepository : IRepository<AdvertComment>
    {
        Task<AdvertComment> GetById(string id);

        //Task Delete(DateTime dateOfCreation, string userId);

        Task<List<AdvertComment>> GetByAdvertId(string advertId);

        //Task<AdvertComment> GetByDateOfCreation(DateTime dateOfCreation, string userId);

        Task Delete(string commentId);
    }
}