using AutoMapper;
using BuyMovies.Models;
using Movies.DAL.Models;

namespace BuyMovies.MapperProfiles
{
    public class ActorProfile:Profile
    {
        public ActorProfile()
        {
            CreateMap<ActorViewModel,Actor>().ReverseMap();
        }
    }
}
