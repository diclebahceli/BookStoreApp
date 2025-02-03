using System;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.AddBooks;
using WebApi.BookOperations.GetBookById;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBooks;
using WebApi.DBOperations;
using static WebApi.BookOperations.AddBooks.CreateBookCommand;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]
public class BookController : ControllerBase
{

    private readonly BookStoreDBContext _context;

    public BookController(BookStoreDBContext context)
    {
        _context = context;
    }


    [HttpGet]
    public IActionResult GetBooks()
    {

        GetBooksQuery query = new GetBooksQuery(_context);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var query = new GetBookByIdQuery(_context);
        query.bookId = id;
        var book = query.Handle();
        return Ok(book);
    }



    // [HttpGet]
    // public Book Get([FromQuery] string id)
    // {
    //     var book = BookList.Where(x => x.Id == Convert.ToInt32(id)).SingleOrDefault();
    //     return book;
    // }



    [HttpPost]
    public IActionResult AddBook([FromBody] CreateBookModel newBook)
    {
        var command = new CreateBookCommand(_context);
        try
        {
            command.Model = newBook;
            command.Handle();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok();
    }



    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
    {
        var command = new UpdateBooksCommand(_context);
        command.id = id;
        command.updateBooksModel = new UpdateBooksModel
        {
            Title = updatedBook.Title,
            GenreId = updatedBook.GenreId,
            PageCount = updatedBook.PageCount,
            PublishDate = updatedBook.PublishDate,
            Author = updatedBook.Author
        };
        command.Handle();
        return Ok();
    }


    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        var book = _context.Books.SingleOrDefault(x => x.Id == id);
        if (book is null)
            return BadRequest();
        _context.Books.Remove(book);
        _context.SaveChanges();
        return Ok();
    }
}
