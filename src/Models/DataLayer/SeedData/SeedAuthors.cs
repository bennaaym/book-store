using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Book_Store.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Models.DataLayer.SeedData
{
  public class SeedAuthors : IEntityTypeConfiguration<Author>
  {
    public void Configure(EntityTypeBuilder<Author> builder)
    {
      builder.HasData(
        new Author { AuthorId = 1, FirstName = "Michelle", LastName = "Alexander"},
        new Author { AuthorId = 2, FirstName = "Stephen E.", LastName = "Ambrose"},
        new Author { AuthorId = 3, FirstName = "Margaret", LastName = "Atwood" },
        new Author { AuthorId = 4, FirstName = "Jane", LastName = "Austen" },
        new Author { AuthorId = 5, FirstName = "James", LastName = "Baldwin" },
        new Author { AuthorId = 6, FirstName = "Emily", LastName = "Bronte" },
        new Author { AuthorId = 7, FirstName = "Agatha", LastName = "Christie" },
        new Author { AuthorId = 8, FirstName = "Ta-Nehisi", LastName = "Coates" },
        new Author { AuthorId = 9, FirstName = "Jared", LastName = "Diamond" },
        new Author { AuthorId = 10, FirstName = "Joan", LastName = "Didion" },
        new Author { AuthorId = 11, FirstName = "Daphne", LastName = "Du Maurier" },
        new Author { AuthorId = 12, FirstName = "Tina", LastName = "Fey" },
        new Author { AuthorId = 13, FirstName = "Roxane", LastName = "Gay" },
        new Author { AuthorId = 14, FirstName = "Dashiel", LastName = "Hammett" },
        new Author { AuthorId = 15, FirstName = "Frank", LastName = "Herbert" },
        new Author { AuthorId = 16, FirstName = "Aldous", LastName = "Huxley" },
        new Author { AuthorId = 17, FirstName = "Stieg", LastName = "Larsson" },
        new Author { AuthorId = 18, FirstName = "David", LastName = "McCullough" },
        new Author { AuthorId = 19, FirstName = "Toni", LastName = "Morrison" },
        new Author { AuthorId = 20, FirstName = "George", LastName = "Orwell" },
        new Author { AuthorId = 21, FirstName = "Mary", LastName = "Shelley" },
        new Author { AuthorId = 22, FirstName = "Sun", LastName = "Tzu" },
        new Author { AuthorId = 23, FirstName = "Augusten", LastName = "Burroughs" },
        new Author { AuthorId = 25, FirstName = "JK", LastName = "Rowling" },
        new Author { AuthorId = 26, FirstName = "Seth", LastName = "Grahame-Smith" }
        );
    }
  }
}
