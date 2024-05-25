using AutoMapper;
using BuyMovies.Models;
using Movies.DAL.Models;

namespace BuyMovies.MapperProfiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
        }
    }
}
