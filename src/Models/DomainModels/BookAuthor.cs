

namespace Book_Store.Models.DomainModels
{
  public class BookAuthor
  {
    public int BookId { get; set; }
    public int AuthorId { get; set; }

    public Author Author { get; set; }
    public Book Book { get; set; }
  }
}
