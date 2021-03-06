using System.ComponentModel.DataAnnotations;


namespace Book_Store.Models.ViewModels
{
  public class RegisterViewModel
  {
    [Required(ErrorMessage = "Please enter a user name")]
    [StringLength(255)]
    public string Username { get; set; }

    [Required(ErrorMessage = "Please enter a password")]
    [DataType(DataType.Password)]
    [Compare("ConfirmPassword")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Please confirm your password")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; }
  }
}
