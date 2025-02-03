using System;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBookById;

public class GetBookByIdQuery
{
    private readonly BookStoreDBContext _context;
    public int bookId;

    public GetBookByIdQuery(BookStoreDBContext bookStoreDBContext)
    {
        _context = bookStoreDBContext;
    }


    public BookModel Handle()
    {
        var book = _context.Books.Include(x => x.Genre).SingleOrDefault(x => x.Id == bookId);
        if (book is null)
            throw new InvalidOperationException("Book does not exist");
        return new BookModel
        {
            Title = book.Title,
            Genre = book.Genre.Name,
            PageCount = book.PageCount,
            PublishDate = book.PublishDate.Date,
            Author = book.Author
        };
    }

}


public class BookModel
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishDate { get; set; }
    public string Author { get; set; }
}
