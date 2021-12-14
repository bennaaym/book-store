using Book_Store.Models.DomainModels;
using Book_Store.Models.DTOs;
using Book_Store.Models.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Models.Grid
{
  public class RouteDictionary : Dictionary<string, string>
  {
    private string Get(string key) => Keys.Contains(key) ? this[key] : null;

    public int PageNumber
    {
      get => Get(nameof(GridDTO.PageNumber)).ToInt();
      set => this[nameof(GridDTO.PageNumber)] = value.ToString();
    }

    public int PageSize
    {
      get => Get(nameof(GridDTO.PageSize)).ToInt();
      set => this[nameof(GridDTO.PageSize)] = value.ToString();
    }

    public string SortField
    {
      get => Get(nameof(GridDTO.SortField));
      set => this[nameof(GridDTO.SortField)] = value;
    }

    public string SortDirection
    {
      get => Get(nameof(GridDTO.SortDirection));
      set => this[nameof(GridDTO.SortDirection)] = value;
    }

    public void SetSortAndDirection(string fieldName, RouteDictionary current)
    {
      this[nameof(GridDTO.SortField)] = fieldName;

      if (current.SortField.EqualsNoCase(fieldName) && current.SortDirection == "asc")
        this[nameof(GridDTO.SortDirection)] = "desc";
      else
        this[nameof(GridDTO.SortDirection)] = "asc";
    }


    public RouteDictionary Clone()
    {
      var clone = new RouteDictionary();
      foreach(var key in Keys)
      {
        clone.Add(key, this[key]);
      }

      return clone;
    }

    public string GenreFilter
    {
      get => Get(nameof(BookGridDTO.Genre))?.Replace(FilterPrefix.Genre, "");
      set => this[nameof(BookGridDTO.Genre)] = value;
    }

    public string PriceFilter
    {
      get => Get(nameof(BookGridDTO.Price))?.Replace(FilterPrefix.Price, "");
      set => this[nameof(BookGridDTO.Price)] = value;
    }

    public string AuthorFilter
    {
      // author filter contains prefix, author id, and slug (eg, author-8-ta-nehisi-coates).
      // only need author id for filtering, so first remove 'author-' prefix from string. At
      // that point, the authorid will be at beginning of string. So find index of dash after 
      // id number and then return substring from beginning of string to that index.
      get
      {
        string s = Get(nameof(BookGridDTO.Author))?.Replace(FilterPrefix.Author, "");
        int index = s?.IndexOf('-') ?? -1;
        return (index == -1) ? s : s.Substring(0, index);
      }
      set => this[nameof(BookGridDTO.Author)] = value;
    }

    public void ClearFilters() => GenreFilter = PriceFilter = AuthorFilter = BookGridDTO.DefaultFilter;
  }
}
