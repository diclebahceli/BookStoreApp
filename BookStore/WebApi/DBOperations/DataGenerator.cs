using System;
using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApi.DBOperations;

public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new BookStoreDBContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDBContext>>()))
        {
            if (context.Books.Any())
            {
                return;
            }

            context.Books.AddRange(
                new Book()
                {
                    Title = "The Great Gatsby",
                    Genre = new Genre() { Id = 1, Name = "Novel" },
                    PageCount = 180,
                    PublishDate = new DateTime(1925, 4, 10)
                },
             new Book()
             {
                 Title = "Moby",
                 Genre = new Genre() { Id = 2, Name = "Novel" },
                 PageCount = 200,
                 PublishDate = new DateTime(1851, 10, 18)
             },
                new Book()
                {
                    Title = "Don Quixote",
                    Genre = new Genre() { Id = 3, Name = "Novel" },
                    PageCount = 863,
                    PublishDate = new DateTime(1605, 1, 16)
                }
            );

            context.SaveChanges();
        }
    }
}
