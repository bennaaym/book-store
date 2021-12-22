using Book_Store.Models.DomainModels;
using Book_Store.Models.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Book_Store.Models.ExtensionMethods
{
  // add a ToDtoList() method to a List of CartItem objects, to make the code
  // that converts it to a list of CartItemDTO objects cleaner.
  public static class CartItemListExtensions
  {
    public static List<CartItemDTO> ToDTO(this List<CartItem> list) =>
      list.Select(ci => new CartItemDTO
      {
        BookId = ci.Book.BookId,
        Quantity = ci.Quantity
      }).ToList();
  }
}
