using Movies.DAL.Data;
using Movies.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuyMovies.Models
{
    public class MovieViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Movie name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Movie description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

       
        public string? ImageURL { get; set; }

        public IFormFile ImageFile { get; set; }

        [Display(Name = "Movie start date")]
        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Movie end date")]
        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Select a category")]
        [Required(ErrorMessage = "Movie category is required")]
        public MovieCategory MovieCategory { get; set; }

        //Relationships
        [Display(Name = "Select actor(s)")]
        [Required(ErrorMessage = "Movie actor(s) is required")]
        public List<int> ActorIds { get; set; }
        public List<Movies_Actors>? Movies_Actors { get; set; }


        [Display(Name = "Select a cinema")]
        [Required(ErrorMessage = "Movie cinema is required")]
        public int CinemaId { get; set; }

        public Cinema? Cinema { get; set;}

        [Display(Name = "Select a producer")]
        [Required(ErrorMessage = "Movie producer is required")]
        public int ProducerId { get; set; }

    }
}
