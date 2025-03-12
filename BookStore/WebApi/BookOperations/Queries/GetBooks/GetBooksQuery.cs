using System;
using AutoMapper;
using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks;

public class GetBooksQuery
{

    private readonly BookStoreDBContext _bookStoreDBContext;
    private readonly IMapper mapper;
    public GetBooksQuery(BookStoreDBContext bookStoreDBContext, IMapper mapper)
    {
        _bookStoreDBContext = bookStoreDBContext;
        this.mapper = mapper;
    }


    public List<BookViewModel> Handle()
    {
        var bookList = _bookStoreDBContext.Books.OrderBy(x => x.Id).Include(x => x.Genre).ToList<Book>();
        var vm = mapper.Map<List<BookViewModel>>(bookList);
        // if (bookList is null)
        //     throw new InvalidOperationException("Book does not exist");
        // foreach (var book in bookList)
        // {
        //     if (book.Genre is null)
        //         throw new InvalidOperationException("Genre does not exist");
        //     vm.Add(mapper.Map<BookViewModel>(book));
        // }
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
