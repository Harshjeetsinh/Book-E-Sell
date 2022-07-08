using BookStoreM.models.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using BookStore.Models.Models;
using System.Linq;

namespace BookStore.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserRepository repository = new UserRepository();

        [HttpGet]
        [Route("list")]
        public IActionResult GetUsers(int pageIndex = 1, int pageSize = 10, string keyword = "")
        {
            try
            {
                var users = repository.GetUsers(pageIndex, pageSize, keyword);
                if (users == null)
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide correct information");

                return StatusCode(HttpStatusCode.OK.GetHashCode(), users);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }

        }

        [Route("~/api/User/Roles")]
        [HttpGet]
        public IActionResult Roles()
        {
            var roles = repository.Roles();
            ListResponse<RoleModel> listResponse = new()
            {
                Results = roles.Results.Select(c => new RoleModel(c)),
                TotalRecords = roles.TotalRecords,
            };
            return Ok(listResponse);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUser(int id)
        {
            try
            {
                var user = repository.GetUser(id);
                if (user == null)
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide correct information");

                return StatusCode(HttpStatusCode.OK.GetHashCode(), user);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateUser(UserModel model)
        {
            try
            {
                if (model != null)
                {
                    var user = repository.GetUser(model.Id);
                    if (user == null)
                        return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "User not found");

                    user.Firstname = model.Firstname;
                    user.Lastname = model.Lastname;
                    user.Email = model.Email;

                    var isSaved = repository.UpdateUser(user);
                    if (isSaved)
                    {
                        return StatusCode(HttpStatusCode.OK.GetHashCode(), "User detail updated successfully");
                    }
                }

                return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide correct information");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                if (id > 0)
                {
                    var user = repository.GetUser(id);
                    if (user == null)
                        return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "User not found");

                    var isDeleted = repository.DeleteUser(user);
                    if (isDeleted)
                    {
                        return StatusCode(HttpStatusCode.OK.GetHashCode(), "User detail deleted successfully");
                    }
                }

                return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide correct information");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
    }
}
