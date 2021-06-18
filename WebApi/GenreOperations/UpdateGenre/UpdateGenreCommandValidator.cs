using FluentValidation;

namespace WebApi.GenreOperations.UpdateGenre
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(command => command.GenreId).GreaterThan(0);
            RuleFor(command => command.GenreViewModel.GenreName).NotEmpty();
        }
    }
}