using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Book_Store.Areas.Admin.Models;
using Book_Store.Models.DataLayer;
using Book_Store.Models.DataLayer.Repositories;
using Book_Store.Models.DomainModels;

namespace Book_Store.Areas.Admin.Controllers
{
  
  [Authorize(Roles ="Admin")]
  [Area("Admin")]
  public class BookController : Controller
  {
    private BookStoreUnitOfWork data { get; set; }
    public BookController(BookstoreContext ctx) => data = new BookStoreUnitOfWork(ctx);

    public IActionResult Index()
    {
      //clear all previous searches
      var search = new SearchData(TempData);
      search.Clear();
      return View();
    }

    [HttpGet]
    public ViewResult Add(int id) => GetBook(id, "Add");

    [HttpPost]
    public IActionResult Add(BookViewModel vm)
    {
      if (ModelState.IsValid)
      {
        data.AddNewBookAuthors(vm.Book, vm.SelectedAuthors);
        data.Books.Insert(vm.Book);
        data.Save();

        TempData["message"] = $"{vm.Book.Title} was added to Books";
        return RedirectToAction("Index");
      }
      else
      {
        Load(vm, "Add");
        return View("Book", vm);
      }
    }

    [HttpGet]
    public ViewResult Edit(int id) => GetBook(id, "Edit");

    [HttpPost]
    public IActionResult Edit (BookViewModel vm)
    {
      if (ModelState.IsValid)
      {
        data.DeleteCurrentBookAuthors(vm.Book);
        data.AddNewBookAuthors(vm.Book, vm.SelectedAuthors);
        data.Books.Update(vm.Book);
        data.Save();
        TempData["message"] = $"{vm.Book.Title} was updated";
        return RedirectToAction("Search");
      }
      else
      {
        Load(vm, "Edit");
        return View("Book", vm);
      }
    }

    [HttpGet]
    public ViewResult Delete(int id) => GetBook(id, "Delete");

    [HttpPost]
    public IActionResult Delete(BookViewModel vm)
    {
      // no ModelState.IsValid check here bc there's no user input - 
      // only BookId in hidden field is posted from form. 
      data.Books.Delete(vm.Book); // cascading delete will get BookAuthors
      data.Save();
      TempData["message"] = $"{vm.Book.Title} was deleted";
      return RedirectToAction("Search");
    }

    [HttpGet]
    public ViewResult Search()
    {
      var search = new SearchData(TempData);

      if (search.HasSearchTerm)
      {
        var vm = new SearchViewModel
        {
          SearchTerm = search.SearchTerm
        };

        var options = new QueryOptions<Book>
        {
          Includes = "Genre, BookAuthors.Author"
        };

        if (search.IsBook)
        {
          options.Where = b => b.Title.Contains(vm.SearchTerm);
          vm.Header = $"Search results for the book title '{vm.SearchTerm}'";
        }

        if (search.IsAuthor)
        {
          // if there's no space, search both first and last name by search term. 
          // Otherwise, assume there's a first and last name and refine search.
          int index = vm.SearchTerm.LastIndexOf(' ');
          if (index == -1) //no space
          {
            options.Where = b => b.BookAuthors.Any(
              ba => ba.Author.FirstName.Contains(vm.SearchTerm) ||
              ba.Author.LastName.Contains(vm.SearchTerm));
          }
          else
          {
            // assume first and last name
            string first = vm.SearchTerm.Substring(0, index);
            string last = vm.SearchTerm.Substring(index + 1); //skip space
            options.Where = b => b.BookAuthors.Any(
              ba => ba.Author.FirstName.Contains(first) &&
              ba.Author.LastName.Contains(last));
          }

          vm.Header = $"Search results for author '{vm.SearchTerm}'";
        }

        if (search.IsGenre)
        {
          options.Where = b => b.GenreId.Contains(vm.SearchTerm);
          vm.Header = $"Search results for the genreId '{vm.SearchTerm}'";
        }

        vm.Books = data.Books.List(options);
        return View("SearchResults", vm);
      }
      else
        return View("Index");
    }

    // search (posted from search text box on Index page). Search term is required field.
    [HttpPost]
    public RedirectToActionResult Search(SearchViewModel vm)
    {
      if (ModelState.IsValid)
      {
        // store search data in TempData and redirect
        var search = new SearchData(TempData)
        {
          SearchTerm = vm.SearchTerm,
          Type = vm.Type
        };
        return RedirectToAction("Search");
      }
      else
        return RedirectToAction("Index");
    }

    private ViewResult GetBook(int id, string operation)
    {
      var book = new BookViewModel();
      Load(book, operation, id);
      return View("Book", book);
    }

    private void Load(BookViewModel vm, string op, int? id = null)
    {
      if (Operation.IsAdd(op))
      {
        vm.Book = new Book();
      }
      else
      {
        vm.Book = data.Books.Get(new QueryOptions<Book>
        {
          Includes = "BookAuthors.Author, Genre",
          Where = b => b.BookId == (id ?? vm.Book.BookId)
        });
      }

      vm.SelectedAuthors = vm.Book.BookAuthors?.Select(ba => ba.Author.AuthorId).ToArray();
      vm.Authors = data.Authors.List(new QueryOptions<Author> { OrderBy = a => a.FirstName });
      vm.Genres = data.Genres.List(new QueryOptions<Genre> { OrderBy = g => g.Name });
    }
  }
}
