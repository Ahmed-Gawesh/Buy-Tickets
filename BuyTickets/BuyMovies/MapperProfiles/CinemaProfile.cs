using AutoMapper;
using BuyMovies.Models;
using Movies.BLL.Repositories;
using Movies.DAL.Models;

namespace BuyMovies.MapperProfiles
{
    public class CinemaProfile:Profile
    {
        public CinemaProfile()
        {
            CreateMap<Cinema,CinemaViewModel>().ReverseMap();
        }
    }
}
