using System;
using FluentValidation;

namespace WebApi.BookOperations.UpdateBooks;

public class UpdateBooksCommandValidation : AbstractValidator<UpdateBooksCommand>
{
    public UpdateBooksCommandValidation()
    {
        RuleFor(command => command.id).GreaterThan(0);
        RuleFor(command => command.updateBooksModel.GenreId).GreaterThan(0);
        RuleFor(command => command.updateBooksModel.PageCount).GreaterThan(0);
        RuleFor(command => command.updateBooksModel.Author).NotEmpty();
        RuleFor(command => command.updateBooksModel.PublishDate).NotEmpty().GreaterThan(DateTime.Now.Date);
        RuleFor(command => command.updateBooksModel.Title).NotEmpty().MinimumLength(4);
    }
}
