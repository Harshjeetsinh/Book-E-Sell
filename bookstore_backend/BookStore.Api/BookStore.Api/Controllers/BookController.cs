﻿using BookStoreM.models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using BookStoreM.models.Models;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("api/book")]
    public class BookController : ControllerBase
    {
        BookRepository bookRepository = new BookRepository();

        [HttpGet]
        [Route("list")]
        public IActionResult GetBooks(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var books = bookRepository.GetBooks(pageIndex, pageSize, keyword);

            ListResponse<BookModel> listResponse = new ListResponse<BookModel>()
            {
                Results = books.Results.Select(c => new BookModel(c)),
                TotalRecords = books.TotalRecords,
            };

            return Ok(listResponse);
        }

        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResult), (int)HttpStatusCode.NotFound)]
        public IActionResult GetBook(int id)
        {
            var book = bookRepository.GetBook(id);
            if (book == null)
                return NotFound();

            return Ok(new BookModel(book));
        }

        [Route("add")]
        [HttpPost]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddBook(BookModel model)
        {
            if (model == null)
                return BadRequest("Model is null");

            Book book = new Book()
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Base64image = model.Base64image,
                Categoryid = model.Categoryid,
                Publisherid = model.Publisherid,
                Quantity = model.Quantity,
            };
            var response = bookRepository.AddBook(book);
            BookModel bookModel = new BookModel(response);

            return Ok(bookModel);
        }

        [Route("update")]
        [HttpPut]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateBook(BookModel model)
        {
            if (model == null)
                return BadRequest("Model is null");

            Book book = new Book()
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Base64image = model.Base64image,
                Categoryid = model.Categoryid,
                Publisherid = model.Publisherid,
                Quantity = model.Quantity,
            };
            var response = bookRepository.UpdateBook(book);
            BookModel bookModel = new BookModel(response);

            return Ok(bookModel);
        }

        [Route("delete/{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult DeleteBook(int id)
        {
            if (id == 0)
                return BadRequest("id is null");

            var response = bookRepository.DeleteBook(id);
            return Ok(response);
        }
    }
}
