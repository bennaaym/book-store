using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
