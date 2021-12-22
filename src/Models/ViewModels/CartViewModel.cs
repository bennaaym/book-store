using Book_Store.Models.DomainModels;
using Book_Store.Models.Grid;
using System.Collections.Generic;


namespace Book_Store.Models.ViewModels
{
  public class CartViewModel
  {
    public IEnumerable<CartItem> List { get; set; }
    public double Subtotal { get; set; }
    public RouteDictionary BookGridRoute { get; set; }

  }
}
