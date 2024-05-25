using Movies.DAL.Models;

namespace BuyMovies.Helpers
{
	public interface IEmailSettings
	{
		public void SendEmail(Email email);
	}
}
