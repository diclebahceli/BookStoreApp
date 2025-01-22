using System;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s/[action]")]
public class BookController : ControllerBase
{
    private static List<Book> BookList = new List<Book>(){
        new Book(){
            Id = 1,
            Title = "The Great Gatsby",
            Genre = new Genre(){Id = 1, Name = "Novel"},
            PageCount = 180,
            PublishDate = new DateTime(1925, 4, 10)
            },
         new Book(){
            Id = 2,
            Title = "Moby",
            Genre = new Genre(){Id = 2, Name = "Novel"},
            PageCount = 200,
            PublishDate = new DateTime(1851, 10, 18)
         },
            new Book(){
                Id = 3,
                Title = "Don Quixote",
                Genre = new Genre(){Id = 3, Name = "Novel"},
                PageCount = 863,
                PublishDate = new DateTime(1605, 1, 16)
            }
    };


    [HttpGet]
    public List<Book> GetBooks()
    {
        var bookList = BookList.OrderBy(x => x.Id).ToList<Book>();
        return bookList;
    }

    [HttpGet("{id}")]
    public Book GetById(int id)
    {
        var book = BookList.Where(x => x.Id == id).SingleOrDefault();
        return book;
    }



    [HttpGet]
    public Book Get([FromQuery] string id)
    {
        var book = BookList.Where(x => x.Id == Convert.ToInt32(id)).SingleOrDefault();
        return book;
    }



    [HttpPost]
    public IActionResult AddBook([FromBody] Book newBook)
    {
        var book = BookList.SingleOrDefault(x => x.Title == newBook.Title);
        if (book != null)
        {
            return BadRequest();
        }
        BookList.Add(newBook);
        return Ok();
    }



    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
    {
        var book = BookList.SingleOrDefault(x => x.Id == id);
        if (book is null)
            return BadRequest();
        book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
        book.Genre = updatedBook.Genre.Id != default ? updatedBook.Genre : book.Genre;
        book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
        book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
        return Ok();
    }
}
