using AutoMapper;
using BusinessLayer.Dtos;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.MapProfiles
{
    internal class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<UserRegisterDto, UserProfile>();
            CreateMap<UserProfile, UserRegisterDto>();
        }
    }
}
