using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Book_Store.Models.DomainModels;
using Book_Store.Models.ViewModels;

namespace Book_Store.Controllers
{
  public class AccountController : Controller
  {
    private UserManager<User> userManager;
    private SignInManager<User> signInManager;

    public AccountController(UserManager<User> userMngr, SignInManager<User> signInMngr)
    {
      userManager = userMngr;
      signInManager = signInMngr;
    }

    [HttpGet]
    public IActionResult Register()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
      var user = new User { UserName = model.Username };
      var result = await userManager.CreateAsync(user, model.Password);

      if (result.Succeeded)
      {
        bool isPersistent = false;
        await signInManager.SignInAsync(user, isPersistent);
        return RedirectToAction("Index", "Home");
      }
      else
      {
        foreach(var error in result.Errors)
        {
          ModelState.AddModelError("", error.Description);
        }
      }

      return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> LogOut()
    {
      await signInManager.SignOutAsync();
      return RedirectToAction("Index", "Home");
    }

    public ViewResult AccessDenied()
    {
      return View();
    }

    [HttpGet]
    public IActionResult LogIn (string returnUrl = "")
    {
      var model = new LoginViewModel { ReturnUrl = returnUrl };
      return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> LogIn (LoginViewModel model)
    {
      if (ModelState.IsValid)
      {
        var result = await signInManager.PasswordSignInAsync(
          model.Username, model.Password, isPersistent: model.RememberMe, lockoutOnFailure: false);

        if (result.Succeeded)
        {
          if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl) && model.ReturnUrl != "/cart/add")
          {
            return Redirect(model.ReturnUrl);
          }
          else
          {
            return RedirectToAction("Index", "Home");
          }
        }       
      }

      ModelState.AddModelError("", "Invalid User Name or Password");
      return View(model);
    }
  }
}
