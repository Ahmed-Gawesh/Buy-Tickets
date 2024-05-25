using System.ComponentModel.DataAnnotations;

namespace BuyMovies.Models
{
    public class ProducerViewModel
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Name Is Required")]
        public string FullName { get; set; }

        public string? ProfilePictureURL { get; set; }

        public IFormFile Picture { get; set; }
        [Display(Name = "Biography")]
        [Required(ErrorMessage = "Bio Is Required")]
        public string Bio { get; set; }
    }
}
