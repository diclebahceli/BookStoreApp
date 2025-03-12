using System;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.BookOperations.DeleteBookCommand;

public class DeleteBookCommand
{
    private readonly BookStoreDBContext _context;
    public int id;


    public DeleteBookCommand(BookStoreDBContext bookStoreDBContext)
    {
        _context = bookStoreDBContext;
    }

    public void Handle()
    {
        var book = _context.Books.Include(a => a.Genre).SingleOrDefault(x => x.Id == id);
        if (book is null)
            throw new InvalidOperationException("Book does not exist");

        _context.Books.Remove(book);
        _context.SaveChanges();
    }

}
