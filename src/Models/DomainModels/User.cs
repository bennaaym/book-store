using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace Book_Store.Models.DomainModels
{
  public class User : IdentityUser
  {
    [NotMapped]
    public IList<string> RoleNames { get; set; }
  }
}
