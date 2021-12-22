using Microsoft.AspNetCore.Identity;
using Book_Store.Models.DomainModels;
using System.Collections.Generic;


namespace Book_Store.Areas.Admin.Models
{
  public class UserViewModel
  {
    public IEnumerable<User> Users { get; set; }
    public IEnumerable<IdentityRole> Roles { get; set; }
  }
}
