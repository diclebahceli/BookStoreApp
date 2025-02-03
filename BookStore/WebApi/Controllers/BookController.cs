using System;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.AddBooks;
using WebApi.BookOperations.DeleteBookCommand;
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
        BookModel res = new BookModel();

        try
        {
            var query = new GetBookByIdQuery(_context);
            query.bookId = id;
            res = query.Handle();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok(res);
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
    public IActionResult UpdateBook(int id, [FromBody] UpdateBooksModel updatedBook)
    {
        try
        {
            var command = new UpdateBooksCommand(_context);
            command.id = id;
            command.updateBooksModel = updatedBook;
            command.Handle();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok();
    }


    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        try
        {
            var command = new DeleteBookCommand(_context);
            command.id = id;
            command.Handle();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok();
    }
}
