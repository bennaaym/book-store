using Book_Store.Models.DomainModels;
using Book_Store.Models.Grid;
using System.Collections.Generic;


namespace Book_Store.Models.ViewModels
{
  public class BookListViewModel
  {
    public IEnumerable<Book> Books { get; set; }
    public RouteDictionary CurrentRoute { get; set; }
    public int TotalPages { get; set; }

    //data for drop downs
    public IEnumerable<Author> Authors { get; set; }
    public IEnumerable<Genre> Genres { get; set; }
    public Dictionary<string, string> Prices =>
      new Dictionary<string, string>
      {
        {"under7", "Under $7" },
        {"7to14", "$7 to $14" },
        {"over14", "Over $14" }
      };

    //data for page size drop down
    public int[] PageSizes => new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
  }
}
