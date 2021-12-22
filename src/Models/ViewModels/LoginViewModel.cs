using System.ComponentModel.DataAnnotations;


namespace Book_Store.Models.ViewModels
{
  public class LoginViewModel
  {
    [Required(ErrorMessage = " ")]
    [StringLength(255)]
    public string Username { get; set; }

    [Required(ErrorMessage = " ")]
    public string Password { get; set; }

    public string ReturnUrl { get; set; }
    public bool RememberMe { get; set; }
  }
}
