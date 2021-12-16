using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Book_Store.Models.DomainModels;
using Book_Store.Models.DataLayer.SeedData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Models.DataLayer
{
    public class BookstoreContext : IdentityDbContext<User>
    {
        public BookstoreContext(DbContextOptions<BookstoreContext> options)
          : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Genre> Genres { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //set primary keys
            modelBuilder.Entity<BookAuthor>().HasKey(ba => new { ba.BookId, ba.AuthorId });

            //set foreign keys
            modelBuilder.Entity<BookAuthor>().HasOne(ba => ba.Book)
              .WithMany(b => b.BookAuthors)
              .HasForeignKey(ba => ba.BookId);
            modelBuilder.Entity<BookAuthor>().HasOne(ba => ba.Author)
              .WithMany(b => b.BookAuthors)
              .HasForeignKey(ba => ba.AuthorId);

            //remove cascading delete with genre
            modelBuilder.Entity<Book>().HasOne(b => b.Genre)
              .WithMany(g => g.Books)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.ApplyConfiguration(new SeedGenres());
            modelBuilder.ApplyConfiguration(new SeedBooks());
            modelBuilder.ApplyConfiguration(new SeedAuthors());
            modelBuilder.ApplyConfiguration(new SeedBookAuthors());
        }

        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
          UserManager<User> userManager = serviceProvider.GetRequiredService<UserManager<User>>();
          RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

          string username = "b181210558@sakarya.edu.tr";
          string password = "123";
          string roleName = "Admin";

          //if role doesn't exist, create it
          if (await roleManager.FindByNameAsync(roleName) == null)
          {
            await roleManager.CreateAsync(new IdentityRole(roleName));
          }

          // if username doesn't exist, create it and add to role
          if (await userManager.FindByNameAsync(username) == null)
          {
            User user = new User { UserName = username };
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
              await userManager.AddToRoleAsync(user, roleName);
            }
          }
        }
    }
}
