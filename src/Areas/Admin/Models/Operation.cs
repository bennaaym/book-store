using Book_Store.Models.ExtensionMethods;


namespace Book_Store.Areas.Admin.Models
{
  // static class used mostly by admin area views to determine which fields to 
  // display, but also used in Admin area book controller and validate class.
  public static class Operation
  {
    public static bool IsAdd(string action) => action.EqualsNoCase("add");
    public static bool IsDelete(string action) => action.EqualsNoCase("delete");
  }
}
