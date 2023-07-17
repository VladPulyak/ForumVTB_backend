using AutoMapper;
using BusinessLayer.Dtos.Authentication;
using DataAccessLayer.Models;

namespace Forum_VTB.MapProfiles
{
    public class UserMapProfiles : Profile
    {
        public UserMapProfiles()
        {
            CreateMap<UserProfile, UserRegisterDto>().ReverseMap();
        }
    }
}
