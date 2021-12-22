using Book_Store.Models.DomainModels;


namespace Book_Store.Models.DataLayer.Repositories
{
  public interface IBookStoreUnitOfWork
  {
    Repository<Book> Books { get; }
    Repository<Author> Authors { get; }
    Repository<BookAuthor> BookAuthors { get; }
    Repository<Genre> Genres { get; }

    void DeleteCurrentBookAuthors(Book book);
    void AddNewBookAuthors(Book book, int[] authorids);
    void Save();
  }
}
