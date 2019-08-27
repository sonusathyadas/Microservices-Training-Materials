using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileUploadAPIDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileUploadAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Book> Create([FromForm]Book book)
        {

            return new Book
            {
                BookName=book.BookName,
                Author=book.Author
            };
        }
    }
}