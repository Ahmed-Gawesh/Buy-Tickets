using System.ComponentModel.DataAnnotations;

namespace BuyMovies.Models
{
    public class CinemaViewModel
    {
        [Key]
        public int Id { get; set; }


        public string? Logo { get; set; }
        public IFormFile LogoFile { get; set; }

        [Display(Name = "Cinema Name")]
        [Required(ErrorMessage = "Cinema name is required")]
        public string FullName { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Cinema description is required")]
        public string Description { get; set; }
    }
}
