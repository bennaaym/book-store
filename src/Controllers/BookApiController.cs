using Book_Store.Models.DataLayer;
using Book_Store.Models.DataLayer.Repositories;
using Book_Store.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Book_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookApiController : ControllerBase
    {
        private BookStoreUnitOfWork data { get; set; }
        public BookApiController(BookstoreContext ctx) => data = new BookStoreUnitOfWork(ctx);
        // GET: api/<BookApiController>
        [HttpGet]
        public IEnumerable<dynamic> Get()
        {
            List<dynamic> res = new List<dynamic>();

            var books = data.Books.List(new QueryOptions<Book>
            {
                Includes = "BookAuthors.Author, Genre",
            });

            foreach (var book in books)
            {
                Dictionary<string, dynamic> dict = new Dictionary<string, dynamic>();

                dict["id"] = book.BookId;
                dict["title"] = book.Title;
                dict["price"] = book.Price;
                dict["genre"] = book.Genre.Name;

                List<string> authors = new List<string>();
                foreach(var ba in book.BookAuthors)
                {
                    authors.Add(ba.Author.FullName);
                }

                dict["authors"] = authors;
                res.Add(dict);
            }
            return res;
        }

        // GET api/<BookApiController>/5
        [HttpGet("{id}")]
        public Dictionary<string,dynamic> Get(int id)
        {
            Dictionary<string, dynamic> dict = new Dictionary<string, dynamic>();
            List<string> authors = new List<string>();

            var book = data.Books.Get(new QueryOptions<Book>
            {
                Includes = "BookAuthors.Author, Genre",
                Where = b => b.BookId == id
            });

            dict["id"] = book.BookId;
            dict["title"] = book.Title;
            dict["price"] = book.Price;
            dict["genre"] = book.Genre.Name;
            foreach (var ba in book.BookAuthors)
            {
                authors.Add(ba.Author.FullName);
            }
            dict["authors"] = authors;

            return dict;
        }
    }
}
