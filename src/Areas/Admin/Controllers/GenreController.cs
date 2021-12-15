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
  [Area("Admin")]
  public class GenreController : Controller
  {
    private Repository<Genre> data { get; set; }
    public GenreController(BookstoreContext ctx) => data = new Repository<Genre>(ctx);

    public ViewResult Index()
    {
      // clear any previous searches
      var search = new SearchData(TempData);
      search.Clear();

      var genres = data.List(new QueryOptions<Genre>
      {
        OrderBy = g => g.Name
      });
      return View(genres);
    }

    private RedirectToActionResult GoToBookSearchResults(string id)
    {
      // store genre search data in TempData and redirect
      var search = new SearchData(TempData)
      {
        SearchTerm = id,
        Type = "genre"
      };

      return RedirectToAction("Search", "Book");
    }

    // view books by genre
    public RedirectToActionResult ViewBooks(string id) => GoToBookSearchResults(id);

    [HttpGet]
    public ViewResult Add() => View("Genre", new Genre());

    [HttpPost]
    public IActionResult Add(Genre genre)
    {
      // server-side version of remote validation 
      var validate = new Validate(TempData);
      if (!validate.IsGenreChecked)
      {
        validate.CheckGenre(genre.GenreId, data);
        if (!validate.IsValid)
        {
          ModelState.AddModelError(nameof(genre.GenreId), validate.ErrorMessage);
        }
      }

      if (ModelState.IsValid)
      {
        data.Insert(genre);
        data.Save();
        validate.ClearGenre();
        TempData["message"] = $"{genre.Name} was added in the database";
        return RedirectToAction("Index");
      }
      else
        return View("Genre", genre);
    }

    [HttpGet]
    public ViewResult Edit(string id) => View("Genre", data.Get(id));

    [HttpPost]
    public IActionResult Edit(Genre genre)
    {
      // no remote validation of genre on edit
      if (ModelState.IsValid)
      {
        data.Update(genre);
        data.Save();
        TempData["message"] = $"{genre.Name} was updated";
        return RedirectToAction("Index");
      }
      else
        return View("Genre", genre);
    }

    [HttpGet]
    public IActionResult Delete(string id)
    {
      // because cascading deletes are turned off when DbContext configured (so don't automatically
      // delete books when delete genre), will get EF foreign key error if try to delete a genre that's
      // associated with any books. Rather than catch and handle error, this code includes books when
      // retrieving the genre to be deleted. Then, if there are any Book objects in the Books property,
      // it redirects the user to the search results page so they can see said books. 
      var genre = data.Get(new QueryOptions<Genre>
      {
        Includes = "Books",
        Where = g => g.GenreId == id
      });

      if (genre.Books.Count > 0)
      {
        TempData["message"] = $"Can't delete genre {genre.Name} because the genre is associated with these books.";
        return GoToBookSearchResults(id);
      }
      else
      {
        return View("Genre", genre);
      }
    }

    [HttpPost]
    public RedirectToActionResult Delete(Genre genre)
    {
      // no ModelState.IsValid check here bc there's no user input - 
      // only GenreId in hidden field is posted from form. 
      data.Delete(genre);
      data.Save();
      TempData["message"] = $"{genre.Name} was deleted";
      return RedirectToAction("Index");
    }
  }
}
