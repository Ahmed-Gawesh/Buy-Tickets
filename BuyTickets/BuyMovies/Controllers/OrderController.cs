using BuyMovies.Cart;
using BuyMovies.Models;
using Microsoft.AspNetCore.Mvc;
using Movies.BLL.Interfaces;
using Movies.BLL.Repositories;
using System.Security.Claims;

namespace BuyMovies.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IOrderRepository orderRepository;
        private readonly ShoppingCart shoppingCart;

        public OrderController(IUnitOfWork unitOfWork,IOrderRepository orderRepository,ShoppingCart shoppingCart)
        {
            this.unitOfWork = unitOfWork;
            this.orderRepository = orderRepository;
            this.shoppingCart = shoppingCart;
        }
        public async Task<IActionResult> Index()
        {

            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await orderRepository.GetOrderForUserAsync(buyerEmail);

            return View(orders);
        }
        public IActionResult ShoppingCart()
        {
            var items = shoppingCart.GetShoppingCartItems();
            shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = shoppingCart,
                ShoppingCartTotal = shoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }

        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await unitOfWork.MovieRepository.GetbyID(id);

            if (item != null)
            {
                shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await unitOfWork.MovieRepository.GetbyID(id);

            if (item != null)
            {
                shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }


        public async Task<IActionResult> CompleteOrder()
        {
            var items = shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await orderRepository.SaveOrderAsync(items, userId, userEmailAddress);
            await shoppingCart.ClearShoppingCartAsync();

            return View("CompleteOrder");
        }
    }
}
