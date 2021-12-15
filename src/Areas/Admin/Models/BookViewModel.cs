using Book_Store.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Areas.Admin.Models
{
  // Rather than try to build a custom validator for the SelectedAuthors property, 
  // this class is validatable. This is an example of class-level validation. It 
  // leads to a two-step validation process, but it's adequate for this app.
  public class BookViewModel : IValidatableObject
  {
    public Book Book { get; set; }
    public IEnumerable<Genre> Genres { get; set; }
    public IEnumerable<Author> Authors { get; set; }
    public int[] SelectedAuthors { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (SelectedAuthors == null)
      {
        yield return new ValidationResult(
          "Please select at least one author",
          new[] { nameof(SelectedAuthors) });
      }
    }
  }
}
