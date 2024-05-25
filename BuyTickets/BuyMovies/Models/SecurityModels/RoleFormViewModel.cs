using System.ComponentModel.DataAnnotations;

namespace BuyMovies.Models
{
    public class RoleFormViewModel
    {

        [Required(ErrorMessage ="Name Is Required")]
        [StringLength(256)]
        public string Name { get; set; }
    }
}
