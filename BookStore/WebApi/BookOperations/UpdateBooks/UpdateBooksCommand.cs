using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;

namespace WebApi.BookOperations.UpdateBooks;

public class UpdateBooksCommand
{
    private readonly BookStoreDBContext _context;
    public UpdateBooksModel updateBooksModel;
    public int id;
    public UpdateBooksCommand(BookStoreDBContext bookStoreDBContext)
    {
        _context = bookStoreDBContext;
    }

    public void Handle()
    {
        var book = _context.Books.SingleOrDefault(x => x.Id == id);
        if (book is null)
            throw new InvalidOperationException("Book does not exist");

        book.Title = updateBooksModel.Title != default ? updateBooksModel.Title : book.Title;
        book.GenreId = updateBooksModel.GenreId != default ? updateBooksModel.GenreId : book.GenreId;
        book.PageCount = updateBooksModel.PageCount != default ? updateBooksModel.PageCount : book.PageCount;
        book.PublishDate = updateBooksModel.PublishDate != default ? updateBooksModel.PublishDate : book.PublishDate;
        book.Author = updateBooksModel.Author != default ? updateBooksModel.Author : book.Author;

        _context.SaveChanges();

    }

}


public class UpdateBooksModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int GenreId { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishDate { get; set; }
    public string Author { get; set; }
}
