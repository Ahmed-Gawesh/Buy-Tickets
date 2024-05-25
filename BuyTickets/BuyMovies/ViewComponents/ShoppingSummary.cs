using BuyMovies.Cart;
using Microsoft.AspNetCore.Mvc;

namespace BuyMovies.ViewComponents
{
    public class ShoppingSummary:ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;
        public ShoppingSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            var items = _shoppingCart.GetShoppingCartItems();

            return View(items.Count);
        }
    }
}
