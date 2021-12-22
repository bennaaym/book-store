using Book_Store.Models.DomainModels;
using Book_Store.Models.Grid;
using System.Collections.Generic;


namespace Book_Store.Models.ViewModels
{
  public class AuthorListViewModel
  {
    public IEnumerable<Author> Authors { get; set; }
    public RouteDictionary CurrentRoute { get; set; }
    public int TotalPages { get; set; }
  }
}
