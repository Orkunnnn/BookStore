using FluentValidation;

namespace WebApi.GenreOperations.GetGenre
{
    public class GetGenreQueryValidator:AbstractValidator<GetGenreQuery>
    {
        public GetGenreQueryValidator()
        {
            RuleFor(query => query.GenreId).GreaterThan(0);
        }
    }
}