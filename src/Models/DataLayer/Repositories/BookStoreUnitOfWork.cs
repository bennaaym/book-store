using Book_Store.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Models.DataLayer.Repositories
{
  public class BookStoreUnitOfWork : IBookStoreUnitOfWork
  {
    private BookstoreContext context;
    public BookStoreUnitOfWork(BookstoreContext ctx)
    {
      context = ctx;
    }

    private Repository<Book> bookData;
    public Repository<Book> Books
    {
      get
      {
        if (bookData == null)
          bookData = new Repository<Book>(context);

        return bookData;
      }
    }

    private Repository<Author> authorData;
    public Repository<Author> Authors 
    { 
      get
      {
        if (authorData == null)
          authorData = new Repository<Author>(context);

        return authorData;
      }
    }

    private Repository<BookAuthor> bookAuthorData;
    public Repository<BookAuthor> BookAuthors
    {
      get
      {
        if (bookAuthorData == null)
          bookAuthorData = new Repository<BookAuthor>(context);

        return bookAuthorData;
      }
    }

    private Repository<Genre> genreData;
    public Repository<Genre> Genres
    {
      get
      {
        if (genreData == null)
          genreData = new Repository<Genre>(context);

        return genreData;
      }
    }

    public void AddNewBookAuthors(Book book, int[] authorids)
    {
      book.BookAuthors = authorids.Select(i => new BookAuthor { Book = book, AuthorId = i }).ToList();
    }

    public void DeleteCurrentBookAuthors(Book book)
    {
      var currentAuthors = BookAuthors.List(new QueryOptions<BookAuthor>
      {
        Where = ba => ba.BookId == book.BookId
      });

      foreach (BookAuthor ba in currentAuthors)
      {
        BookAuthors.Delete(ba);
      }
    }

    public void Save() => context.SaveChanges();
  }
}
