using Movies.DAL.Models;

namespace BuyMovies.Models
{
    public class DropDownVM
    {
        public List<Cinema> Cinemas { get; set; }
        public List<Actor> Actors { get; set; }
        public List<Producer> Producers { get; set; }
    }

}
