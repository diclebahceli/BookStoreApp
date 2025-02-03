using System;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;

namespace WebApi.BookOperations.AddBooks;

public class CreateBookCommand
{
    private readonly BookStoreDBContext _context;
    public CreateBookModel Model { get; set; }
    public CreateBookCommand(BookStoreDBContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        var book = _context.Books.SingleOrDefault(x => x.Title == Model.Title);
        if (book is not null)
            throw new InvalidOperationException("Book already exist");
        var newBook = new Book
        {
            Title = Model.Title,
            GenreId = Model.GenreId,
            PageCount = Model.PageCount,
            PublishDate = Model.PublishDate.Date,
            Author = Model.Author
        };
        _context.Books.Add(newBook);
        _context.SaveChanges();
    }


    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public string Author { get; set; }
    }
}
