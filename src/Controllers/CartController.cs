using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Book_Store.Models.DataLayer;
using Book_Store.Models.DataLayer.Repositories;
using Book_Store.Models.DomainModels;
using Book_Store.Models.DTOs;
using Book_Store.Models.Grid;
using Book_Store.Models.ViewModels;

namespace Book_Store.Controllers
{

  [Authorize]
  public class CartController : Controller
  {
    private Repository<Book> data { get; set; }

    public CartController(BookstoreContext ctx) => data = new Repository<Book>(ctx);

    private Cart GetCart()
    {
      var cart = new Cart(HttpContext);
      cart.Load(data);
      return cart;
    }

    public IActionResult Index()
    {
      // create a new Cart object and get items from session or restore from cookie and db
      Cart cart = GetCart();

      // create a new builder object to work with route data in session
      var builder = new BooksGridBuilder(HttpContext.Session);

      // create a new view model object with cart and route information and pass it to the view
      var vm = new CartViewModel
      {
        List = cart.List,
        Subtotal = cart.Subtotal,
        BookGridRoute = builder.CurrentRoute
      };

      return View(vm);
    }

    [HttpPost]
    public RedirectToActionResult Add(int id)
    {
      // get the book the user chose from the database
      var book = data.Get(new QueryOptions<Book>
      {
        Includes = "BookAuthors.Author, Genre",
        Where = b => b.BookId == id
      });

      if (book == null)
      {
        TempData["message"] = "Unable to add book to cart";
      }
      else
      {
        // create and load a new Book DTO, then add it to a new CartItem object
        // with a default quantity of one.
        var dto = new BookDTO();
        dto.Load(book);
        CartItem item = new CartItem
        {
          Book = dto,
          Quantity = 1 //default quantity
        };

        // add new item to cart and save to session state
        Cart cart = GetCart();
        cart.Add(item);
        cart.Save();

        TempData["message"] = $"{book.Title} was added to cart";
      }

      // create a new builder object to work with route data in session, then 
      // redirect to Book/List action method, passing a dictionary of route segment 
      // values to build URL 
      var builder = new BooksGridBuilder(HttpContext.Session);
      return RedirectToAction("List", "Book", builder.CurrentRoute);
    }

    [HttpPost]
    public RedirectToActionResult Remove(int id)
    {
      Cart cart = GetCart();
      CartItem item = cart.GetById(id);
      cart.Remove(item);
      cart.Save();

      TempData["message"] = $"{item.Book.Title} was removed from the cart";
      return RedirectToAction("Index");
    }

    [HttpPost]
    public RedirectToActionResult Clear()
    {
      Cart cart = GetCart();
      cart.Clear();
      cart.Save();

      TempData["message"] = "Cart has been cleared";
      return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
      // get selected cart item from session and pass it to the view
      Cart cart = GetCart();
      CartItem item = cart.GetById(id);

      if (item == null)
      {
        TempData["message"] = "Unable to locate cart item";
        return RedirectToAction("Index");
      }
      else
      {
        return View(item);
      }
    }

    [HttpPost]
    public RedirectToActionResult Edit(CartItem item)
    {
      Cart cart = GetCart();
      cart.Edit(item);
      cart.Save();

      TempData["message"] = $"{item.Book.Title} has been updated";
      return RedirectToAction("Index");
    }

    public ViewResult Checkout() => View();
  }
}
