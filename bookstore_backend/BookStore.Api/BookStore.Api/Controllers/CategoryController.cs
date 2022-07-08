using BookStore.Repository;
using BookStoreM.models.Models;
using BookStoreM.models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace BookStore.Api.Controllers
{
    [Route("api/Category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        CategoryRepository categoryRepository = new CategoryRepository();

        [Route("list")]
        [HttpGet]
        [ProducesResponseType(typeof(ListResponse<CategoryModel>), (int)HttpStatusCode.OK)]
        public IActionResult GetCategories()
        {
            var categories = categoryRepository.GetCategories();
            ListResponse<CategoryModel> listResponse = new ListResponse<CategoryModel>()
            {
                Results = categories.Results.Select(c => new CategoryModel(c)),
                TotalRecords = categories.TotalRecords,
            };

            return Ok(listResponse);
        }

        [Route("extra/list")]
        [HttpGet]
        [ProducesResponseType(typeof(ListResponse<CategoryModel>), (int)HttpStatusCode.OK)]
        public IActionResult GetCategorie(int pageIndex = 1, int pageSize = 10, string keyword = "")
        {
          var categories = categoryRepository.GetCategorie(pageIndex, pageSize, keyword);
            ListResponse<CategoryModel> listResponse = new ListResponse<CategoryModel>()
            {
                Results = categories.Results.Select(c => new CategoryModel(c)),
                TotalRecords = categories.TotalRecords,
            };

            return Ok(listResponse);
        }


        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(CategoryModel), (int)HttpStatusCode.OK)]
        public IActionResult GetCategory(int id)
        {
            var category = categoryRepository.GetCategory(id);
            CategoryModel categoryModel = new CategoryModel(category);

            return Ok(categoryModel);
        }

        [Route("Add")]
        [HttpPost]
        [ProducesResponseType(typeof(CategoryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddCategory(CategoryModel model)
        {
            if(model == null)
                return BadRequest("Model is null");

            Category category = new Category()
            {
                Id = model.Id,
                Name = model.Name
            };
            var response = categoryRepository.AddCategory(category);
            CategoryModel categoryModel = new CategoryModel(response);

            return Ok(categoryModel);
        }

        [Route("Update")]
        [HttpPut]
        [ProducesResponseType(typeof(CategoryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateCategory(CategoryModel model)
        {
            if (model == null)
                return BadRequest("Model is null");

            Category category = new Category()
            {
                Id = model.Id,
                Name = model.Name
            };
            var response = categoryRepository.UpdateCategory(category);
            CategoryModel categoryModel = new CategoryModel(response);

            return Ok(categoryModel);
        }
        [Route("delete/{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult DeleteCategory(int id)
        {
            if(id == 0)
                return BadRequest("Id is null ");

            var response = categoryRepository.DeleteCategory(id);
            return Ok(response);
        }
    }
}
    