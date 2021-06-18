using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.DbOperations;
using WebApi.GenreOperations.CreateGenre;
using WebApi.GenreOperations.DeleteGenre;
using WebApi.GenreOperations.GetGenre;
using WebApi.GenreOperations.GetGenres;
using WebApi.GenreOperations.UpdateGenre;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly BookStoreDbContext _bookStoreDbContext;

        public GenresController(BookStoreDbContext bookStoreDbContext, IMapper mapper)
        {
            _bookStoreDbContext = bookStoreDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllGenres()
        {
            var getGenresQuery = new GetGenresQuery(_bookStoreDbContext, _mapper);
            return Ok(getGenresQuery.Handle());
        }

        [HttpGet("id")]
        public IActionResult GetGenreById(int id)
        {
            var getGenreQuery = new GetGenreQuery(_mapper, _bookStoreDbContext) { GenreId = id };
            var validator = new GetGenreQueryValidator();
            validator.ValidateAndThrow(getGenreQuery);
            return Ok(getGenreQuery.Handle());
        }

        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreViewModel createGenreModel)
        {
            var createGenreCommand = new CreateGenreCommand(_bookStoreDbContext, _mapper)
            {
                GenreModel = createGenreModel
            };
            var validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(createGenreCommand);
            createGenreCommand.Handle();
            return Ok();
        }

        [HttpPut("id")]
        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreViewModel viewModel)
        {
            var updateGenreCommand = new UpdateGenreCommand(_bookStoreDbContext, _mapper)
            {
                GenreId = id,
                GenreViewModel = viewModel
            };
            var validator = new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(updateGenreCommand);
            updateGenreCommand.Handle();
            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteGenre(int id)
        {
            var deleteGenreCommand = new DeleteGenreCommand(_bookStoreDbContext) { GenreId = id };
            var validator = new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(deleteGenreCommand);
            deleteGenreCommand.Handle();
            return Ok();
        }
    }
}
