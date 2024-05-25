using Movies.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace BuyMovies.Models
{
    public class ActorViewModel
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Full Name")]
        [Required(ErrorMessage ="Name Is Required")]
        public string FullName { get; set; }


        public IFormFile Picture { get; set; }
        public string? ProfilePictureURL { get; set; }
     

        [Display(Name ="Biography")]
        [Required(ErrorMessage = "Bio Is Required")]
        public string Bio { get; set; }

    }
}
