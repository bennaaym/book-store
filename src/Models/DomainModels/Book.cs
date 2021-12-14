using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Models.DomainModels
{
  public class Book
  {
    public int BookId { get; set; }

    [Required(ErrorMessage = "Please enter title")]
    [MaxLength(200)]
    public string Title { get; set; }

    [Range(0.0,1000000.0, ErrorMessage ="Price must be more than 0 and less than 1 million")]
    public double Price { get; set; }

    [Required(ErrorMessage ="Please select genre")]
    public string GenreId { get; set; }

    public Genre Genre { get; set; }
    public ICollection<BookAuthor> BookAuthors { get; set; }
  }
}
