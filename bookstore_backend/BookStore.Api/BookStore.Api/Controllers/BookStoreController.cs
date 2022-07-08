using BookStoreM.models.Models;
using BookStoreM.models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BookStore.Api.Controllers
{
    [Route("api/public")]
    [ApiController]
    public class BookStoreController : ControllerBase
    {
        UserRepository repository = new UserRepository();

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginModel model)
        {
            try
            {
                User user = repository.Login(model);
                if (user == null)
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "User not found");

                return StatusCode(HttpStatusCode.OK.GetHashCode(), user);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterModel model)
        {
            try
            {
                User user = repository.Register(model);
                if (user == null)
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Bad request");

                return StatusCode(HttpStatusCode.OK.GetHashCode(), user);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
    }
}
