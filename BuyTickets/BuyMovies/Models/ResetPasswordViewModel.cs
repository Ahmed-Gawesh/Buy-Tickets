using System.ComponentModel.DataAnnotations;

namespace BuyMovies.Models
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Password Is Required")]
        public string NewPassword { get; set; }


        [Required(ErrorMessage = "Password Is Required")]
        [Compare("NewPassword", ErrorMessage = "Confirm Password Does Not Match New Password ")]
        public string ConfirmPassword { get; set; }
    }
}
