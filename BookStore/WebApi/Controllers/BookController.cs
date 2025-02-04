using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using BookStore.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.AddBooks;
using WebApi.BookOperations.CreateBook;
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
    private readonly IMapper mapper;

    public BookController(BookStoreDBContext context, IMapper mapper)
    {
        _context = context;
        this.mapper = mapper;
    }


    [HttpGet]
    public IActionResult GetBooks()
    {

        GetBooksQuery query = new GetBooksQuery(_context, mapper);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        BookModel res = new BookModel();

        try
        {
            var query = new GetBookByIdQuery(_context, mapper);
            query.bookId = id;
            GetBookByIdQueryValidator validator = new GetBookByIdQueryValidator();
            validator.ValidateAndThrow(query);
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
        var command = new CreateBookCommand(_context, mapper);
        try
        {
            command.Model = newBook;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            // var res = validator.Validate(command);

            // if (!res.IsValid)
            //     foreach (var item in res.Errors)
            //         Console.WriteLine("Property: " + item.PropertyName + " Error Message: " + item.ErrorMessage);

            // else
            //     command.Handle();
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
            UpdateBooksCommandValidation validator = new UpdateBooksCommandValidation();
            validator.ValidateAndThrow(command);
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
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok();
    }
}
