using System;
using AutoMapper;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApi.BookOperations.GetBooks;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBookById;

public class GetBookByIdQuery
{
    private readonly BookStoreDBContext _context;
    private readonly IMapper mapper;
    public int bookId;

    public GetBookByIdQuery(BookStoreDBContext bookStoreDBContext, IMapper mapper)
    {
        _context = bookStoreDBContext;
        this.mapper = mapper;
    }


    public BookModel Handle()
    {
        var book = _context.Books.Include(x => x.Genre).SingleOrDefault(x => x.Id == bookId);
        if (book is null)
            throw new InvalidOperationException("Book does not exist");
        return mapper.Map<BookModel>(book);
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
