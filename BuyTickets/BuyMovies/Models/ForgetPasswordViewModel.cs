using System.ComponentModel.DataAnnotations;

namespace BuyMovies.Models
{
	public class ForgetPasswordViewModel
	{
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }
	}
}
