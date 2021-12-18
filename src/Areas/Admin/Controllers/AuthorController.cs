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
using Microsoft.AspNetCore.Mvc.Localization;

namespace Book_Store.Areas.Admin.Controllers
{

  [Authorize(Roles ="Admin")]
  [Area("Admin")]
  public class AuthorController : Controller
  {

    private readonly IHtmlLocalizer<AuthorController> _localizer;
    private Repository<Author> data { get; set; }
    public AuthorController(BookstoreContext ctx, IHtmlLocalizer<AuthorController> localizer)
    {
        data = new Repository<Author>(ctx);
        _localizer = localizer;

    }

    public ViewResult Index()
    {
      var authors = data.List(new QueryOptions<Author>
      {
        OrderBy = a => a.FirstName
      });

      return View(authors);
    }

    // select (posted from author drop down on Index page). 
    public RedirectToActionResult Select(int id, string operation)
    {

      if(operation.ToLower() == _localizer["Books"].Value.ToLower())
                return RedirectToAction("ViewBooks", new { id });
      if(operation.ToLower() == _localizer["Edit"].Value.ToLower())
                return RedirectToAction("Edit", new { id });
      if(operation.ToLower() == _localizer["Delete"].Value.ToLower())
                return RedirectToAction("Delete", new { id });
      else
                return RedirectToAction("Index");
    }

    private RedirectToActionResult GoToAuthorSearch(Author author)
    {
      // store author search data in TempData and redirect
      var search = new SearchData(TempData)
      {
        SearchTerm = author.FirstName,
        Type = "author"
      };

      return RedirectToAction("Search", "Book");
    }

    // view books by author
    public RedirectToActionResult ViewBooks(int id)
    {
      var author = data.Get(id);
      return GoToAuthorSearch(author);
    }

    [HttpGet]
    public ViewResult Add() => View("Author", new Author());

    [HttpPost]
    public IActionResult Add(Author author, string operation)
    {
      // server-side version of remote validation 
      var validate = new Validate(TempData);
      if (!validate.IsAuthorChecked)
      {
        validate.CheckAuthor(author.FirstName, author.LastName, operation, data);
        if (!validate.IsValid)
        {
          ModelState.AddModelError(nameof(author.LastName), validate.ErrorMessage);
        }
      }

      if (ModelState.IsValid)
      {
        data.Insert(author);
        data.Save();
        validate.ClearAuthor();
        TempData["message"] = $"{author.FullName} was added in the database";
        return RedirectToAction("Index");
      }
      else
        return View("Author", author);
    }

    [HttpGet]
    public ViewResult Edit(int id) => View("Author", data.Get(id));

    [HttpPost]
    public IActionResult Edit(Author author)
    {
      // no remote validation of author on edit
      if (ModelState.IsValid)
      {
        data.Update(author);
        data.Save();
        TempData["message"] = $"{author.FullName} was updated";
        return RedirectToAction("Index");
      }
      else
        return View("Author", author);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
      var author = data.Get(new QueryOptions<Author>
      {
        Includes = "BookAuthors",
        Where = a => a.AuthorId == id
      });

      if (author.BookAuthors.Count > 0)
      {
        TempData["message"] = $"Can't delete author {author.FullName} because the author is associated with these books.";
        return GoToAuthorSearch(author);
      }
      else
      {
        return View("Author", author);
      }
    }

    [HttpPost]
    public RedirectToActionResult Delete(Author author)
    {
      // no ModelState.IsValid check here bc there's no user input - 
      // only AuthorId in hidden field is posted from form. 
      data.Delete(author);
      data.Save();
      TempData["message"] = $"{author.FullName} was deleted";
      return RedirectToAction("Index");
    }
  }
}
