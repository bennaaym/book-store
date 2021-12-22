using Newtonsoft.Json;


namespace Book_Store.Models.DTOs
{
  public class BookGridDTO : GridDTO
  {
    [JsonIgnore]
    public const string DefaultFilter = "all";

    public string Author { get; set; } = DefaultFilter;
    public string Genre { get; set; } = DefaultFilter;
    public string Price { get; set; } = DefaultFilter;
  }
}
