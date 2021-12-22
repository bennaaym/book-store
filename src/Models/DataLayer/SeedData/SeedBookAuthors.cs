using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Book_Store.Models.DomainModels;



namespace Book_Store.Models.DataLayer.SeedData
{
  public class SeedBookAuthors : IEntityTypeConfiguration<BookAuthor>
  {
    public void Configure(EntityTypeBuilder<BookAuthor> builder)
    {
      builder.HasData(
        new BookAuthor { BookId = 1, AuthorId = 18 },
        new BookAuthor { BookId = 2, AuthorId = 20},
        new BookAuthor { BookId = 3, AuthorId = 7 },
        new BookAuthor { BookId = 4, AuthorId = 2 },
        new BookAuthor { BookId = 5, AuthorId = 19 },
        new BookAuthor { BookId = 6, AuthorId = 8 },
        new BookAuthor { BookId = 7, AuthorId = 12 },
        new BookAuthor { BookId = 8, AuthorId = 16 },
        new BookAuthor { BookId = 9, AuthorId = 2 },
        new BookAuthor { BookId = 10, AuthorId = 20 },
        new BookAuthor { BookId = 11, AuthorId = 15 },
        new BookAuthor { BookId = 12, AuthorId = 4 },
        new BookAuthor { BookId = 13, AuthorId = 21 },
        new BookAuthor { BookId = 14, AuthorId = 5 },
        new BookAuthor { BookId = 15, AuthorId = 9 },
        new BookAuthor { BookId = 16, AuthorId = 13 },
        new BookAuthor { BookId = 17, AuthorId = 7 },
        new BookAuthor { BookId = 18, AuthorId = 4 },
        new BookAuthor { BookId = 19, AuthorId = 11 },
        new BookAuthor { BookId = 20, AuthorId = 22 },
        new BookAuthor { BookId = 21, AuthorId = 17 },
        new BookAuthor { BookId = 22, AuthorId = 3 },
        new BookAuthor { BookId = 23, AuthorId = 14 },
        new BookAuthor { BookId = 24, AuthorId = 1 },
        new BookAuthor { BookId = 25, AuthorId = 10 },
        new BookAuthor { BookId = 26, AuthorId = 6 },
        new BookAuthor { BookId = 27, AuthorId = 23 },
        new BookAuthor { BookId = 28, AuthorId = 4 },
        new BookAuthor { BookId = 28, AuthorId = 26 },
        new BookAuthor { BookId = 29, AuthorId = 25 }
        );
    }
  }
}
