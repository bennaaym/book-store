using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Models.ViewModels
{
  public class LoginViewModel
  {
    [Required(ErrorMessage = "Please enter a user name")]
    [StringLength(255)]
    public string Username { get; set; }

    [Required(ErrorMessage = "Please enter a password")]
    public string Password { get; set; }

    public string ReturnUrl { get; set; }
    public bool RememberMe { get; set; }
  }
}
