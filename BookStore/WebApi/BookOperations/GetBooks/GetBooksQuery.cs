using System;
using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks;

public class GetBooksQuery
{

    private readonly BookStoreDBContext _bookStoreDBContext;
    public GetBooksQuery(BookStoreDBContext bookStoreDBContext)
    {
        _bookStoreDBContext = bookStoreDBContext;
    }


    public List<BookViewModel> Handle()
    {
        var bookList = _bookStoreDBContext.Books.OrderBy(x => x.Id).Include(x => x.Genre).ToList<Book>();
        var vm = new List<BookViewModel>();
        if (bookList is null)
            throw new InvalidOperationException("Book does not exist");
        foreach (var book in bookList)
        {
            if (book.Genre is null)
                throw new InvalidOperationException("Genre does not exist");
            vm.Add(
                new BookViewModel
                {
                    Title = book.Title,
                    Genre = book.Genre.Name,
                    PageCount = book.PageCount,
                    PublishDate = book.PublishDate,
                    Author = book.Author,
                }

            );
        }
        return vm;
    }
}


public class BookViewModel
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishDate { get; set; }
    public string Author { get; set; }
}
