using BookStoreM.models.ViewModels;
using BookStoreM.models.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : Controller
    {
        private readonly CartRepository cartRepository = new();

        [HttpGet]
        [Route("list")]
        public IActionResult GetCartItems(string keyword)
        {
            List<Cart> carts = cartRepository.GetCartItems(keyword);
            IEnumerable<CartModel> cartModels = carts.Select(c => new CartModel(c));
            return Ok(cartModels);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddCart(CartModel model)
        {
            if (model == null)
                return BadRequest();

            Cart cart = new Cart()
            {
                Id = model.Id,
                Quantity = 1,
                Bookid = model.BookId,
                Userid = model.UserId
            };
            cart = cartRepository.AddCart(cart);

            return Ok(new CartModel(cart));
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateCart(CartModel model)
        {
            if (model == null)
                return BadRequest();

            Cart cart = new Cart()
            {
                Id = model.Id,
                Quantity = model.Quantity,
                Bookid = model.BookId,
                Userid = model.UserId
            };
            cart = cartRepository.UpdateCart(cart);

            return Ok(new CartModel(cart));
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteCart(int id)
        {
            if (id == 0)
                return BadRequest();

            bool response = cartRepository.DeleteCart(id);
            return Ok(response);
        }
    }
}
