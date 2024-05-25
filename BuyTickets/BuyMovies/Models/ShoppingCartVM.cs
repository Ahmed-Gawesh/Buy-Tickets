using BuyMovies.Cart;

namespace BuyMovies.Models
{
    public class ShoppingCartVM
    {
        public ShoppingCart ShoppingCart { get; set; }
        public decimal ShoppingCartTotal { get; set; }
    }
}
