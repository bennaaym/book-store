using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Models.DomainModels
{
  public class Genre
  {
    [MaxLength(10)]
    [Required(ErrorMessage = "Please enter genre id")]
    public string GenreId { get; set; }

    [StringLength(25)]
    [Required(ErrorMessage ="Please enter genre name")]
    public string Name { get; set; }

    public ICollection<Book> Books { get; set; }
  }
}
