using AutoMapper;
using BuyMovies.Models;
using Movies.DAL.Models;

namespace BuyMovies.MapperProfiles
{
    public class MovieProfile:Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie,MovieViewModel>().ReverseMap();
           
        }
    }
}
