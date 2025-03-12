using System;
using System.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApi.BookOperations.AddBooks;
using static WebApi.BookOperations.AddBooks.CreateBookCommand;

namespace WebApi.BookOperations.CreateBook;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(command => command.Model.GenreId).GreaterThan(0);
        RuleFor(command => command.Model.PageCount).GreaterThan(0);
        RuleFor(command => command.Model.Author).NotEmpty();
        RuleFor(command => command.Model.PublishDate).NotEmpty().GreaterThan(DateTime.Now.Date);
        RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
    }
}
