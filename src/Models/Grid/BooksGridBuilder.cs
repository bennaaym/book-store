using Microsoft.AspNetCore.Http;
using Book_Store.Models.DomainModels;
using Book_Store.Models.DTOs;
using Book_Store.Models.ExtensionMethods;


namespace Book_Store.Models.Grid
{
  // inherits the general purpose GridBuilder class and adds application-specific 
  // methods for loading and clearing filter route segments in route dictionary.
  // Also adds application-specific Boolean flags for sorting and filtering. 
  public class BooksGridBuilder : GridBuilder
  {
    // this constructor gets current route data from session
    public BooksGridBuilder(ISession sess) : base(sess) { }

    // this constructor stores filtering route segments, as well as
    // the paging and sorting route segments stored by the base constructor
    public BooksGridBuilder(ISession sess, BookGridDTO values, string defaultSortFilter) : base (sess, values, defaultSortFilter)
    {
      // store filter route segments - add filter prefixes if this is initial load
      // of page with default values rather than route values (route values have prefix)
      bool isInitial = values.Genre.IndexOf(FilterPrefix.Genre) == -1;
      Routes.AuthorFilter = (isInitial) ? FilterPrefix.Author + values.Author : values.Author;
      Routes.GenreFilter = (isInitial) ? FilterPrefix.Genre + values.Genre : values.Genre;
      Routes.PriceFilter = (isInitial) ? FilterPrefix.Price + values.Price : values.Price;

      SaveRouteSegment();
    }

    // load new filter route segments contained in a string array - add filter prefix 
    // to each one. if filtering by author (rather than just 'all'), add author slug 
    public void LoadFilterSegments(string[] filter, Author author)
    {
      if (author == null)
        Routes.AuthorFilter = FilterPrefix.Author + filter[0];
      else
        Routes.AuthorFilter = FilterPrefix.Author + filter[0] + "-" + author.FullName.Slug();

      Routes.GenreFilter = FilterPrefix.Genre + filter[1];
      Routes.PriceFilter = FilterPrefix.Price + filter[2];
    }

    public void ClearFilterSegments() => Routes.ClearFilters();

    //filter flags
    string def = BookGridDTO.DefaultFilter;
    public bool IsFilterByAuthor => Routes.AuthorFilter != def;
    public bool IsFilterByGenre => Routes.GenreFilter != def;
    public bool IsFilterByPrice => Routes.PriceFilter != def;

    //sort flags
    public bool IsSortByGenre => Routes.SortField.EqualsNoCase(nameof(Genre));
    public bool IsSortByPrice => Routes.SortField.EqualsNoCase(nameof(Book.Price));
  }
}
