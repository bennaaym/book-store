using Microsoft.AspNetCore.Http;
using Book_Store.Models.DTOs;
using Book_Store.Models.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Models.Grid
{
  public class GridBuilder
  {
    private const string RouteKey = "currentroute";
    public RouteDictionary Routes { get; set; }
    public ISession Session { get; set; }

    public GridBuilder(ISession sess)
    {
      Session = sess;
      Routes = Session.GetObject<RouteDictionary>(RouteKey) ?? new RouteDictionary();
    }

    public GridBuilder(ISession sess, GridDTO values, string defaultSortField)
    {
      Session = sess;
      Routes = new RouteDictionary();
      Routes.PageNumber = values.PageNumber;
      Routes.PageSize = values.PageSize;
      Routes.SortField = values.SortField ?? defaultSortField;
      Routes.SortDirection = values.SortDirection;

      SaveRouteSegment();
    }

    public void SaveRouteSegment() =>
      Session.SetObject<RouteDictionary>(RouteKey, Routes);

    public int GetTotalPages(int count)
    {
      int size = Routes.PageSize;
      return (count + size - 1) / size;
    }

    public RouteDictionary CurrentRoute => Routes;

  }
}
