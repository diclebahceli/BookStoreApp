using System;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.BookOperations.GetBooks;
using WebApi.DBOperations;

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
    public Book GetById(int id)
    {
        var book = _context.Books.Where(x => x.Id == id).SingleOrDefault();
        return book;
    }



    // [HttpGet]
    // public Book Get([FromQuery] string id)
    // {
    //     var book = BookList.Where(x => x.Id == Convert.ToInt32(id)).SingleOrDefault();
    //     return book;
    // }



    [HttpPost]
    public IActionResult AddBook([FromBody] Book newBook)
    {
        var book = _context.Books.SingleOrDefault(x => x.Title == newBook.Title);
        if (book != null)
        {
            return BadRequest();
        }
        _context.Books.Add(newBook);
        _context.SaveChanges();
        return Ok();
    }



    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
    {
        var book = _context.Books.SingleOrDefault(x => x.Id == id);
        if (book is null)
            return BadRequest();
        book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
        book.Genre = updatedBook.Genre.Id != default ? updatedBook.Genre : book.Genre;
        book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
        book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
        book.Author = updatedBook.Author != default ? updatedBook.Author : book.Author;
        _context.SaveChanges();
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
