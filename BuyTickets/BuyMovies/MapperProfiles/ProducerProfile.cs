using AutoMapper;
using BuyMovies.Models;
using Movies.DAL.Models;

namespace BuyMovies.MapperProfiles
{
    public class ProducerProfile:Profile
    {
        public ProducerProfile()
        {
            CreateMap<ProducerViewModel,Producer>().ReverseMap();
        }
    }
}
