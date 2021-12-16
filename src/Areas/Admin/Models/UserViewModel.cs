using Microsoft.AspNetCore.Identity;
using Book_Store.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Areas.Admin.Models
{
  public class UserViewModel
  {
    public IEnumerable<User> Users { get; set; }
    public IEnumerable<IdentityRole> Roles { get; set; }
  }
}
