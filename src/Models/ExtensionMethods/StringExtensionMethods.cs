using System.Text;


namespace Book_Store.Models.ExtensionMethods
{
  public static class StringExtension
  {
    public static string Slug(this string s)
    {
      var sb = new StringBuilder();
      foreach(char c in s)
      {
        if (c == '-' || !char.IsPunctuation(c))
        {
          sb.Append(c);
        }
      }

      return sb.ToString().Replace(' ', '-').ToLower();
    }

    public static bool EqualsNoCase(this string s, string tocompare) =>
      s?.ToLower() == tocompare?.ToLower();

    public static int ToInt(this string s)
    {
      int.TryParse(s, out int id);
      return id;
    }

    public static string Capitalize(this string s) =>
      s?.Substring(0, 1)?.ToUpper() + s?.Substring(1);
  }
}
