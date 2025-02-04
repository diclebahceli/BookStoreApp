using System;
using System.Data;
using FluentValidation;

namespace WebApi.BookOperations.DeleteBookCommand;

public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        RuleFor(command => command.id).GreaterThan(0);
    }
}
