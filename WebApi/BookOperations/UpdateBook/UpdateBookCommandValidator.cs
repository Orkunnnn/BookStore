using System;
using FluentValidation;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommandValidator:AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(command => command.BookModel.GenreId).GreaterThan(0);
            RuleFor(command => command.BookModel.PageCount).GreaterThan(0);
            RuleFor(command => command.BookModel.PublishDate).LessThan(DateTime.Now);
            RuleFor(command => command.BookModel.Title).NotEmpty();
            RuleFor(command => command.BookId).GreaterThan(0);
        }
    }
}