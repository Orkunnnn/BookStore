namespace WebApi.Controllers
{
    using AutoMapper;
    using BookOperations.CreateBook;
    using BookOperations.DeleteBook;
    using BookOperations.GetBook;
    using BookOperations.GetBooks;
    using BookOperations.UpdateBook;
    using DbOperations;
    using FluentValidation;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        private readonly IMapper _mapper;

        public BooksController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            var query = new GetBooksQuery(_context, _mapper);
            return Ok(query.Handle());
        }

        [HttpGet("id")]
        public IActionResult GetBookById(int id)
        {
            var query = new GetBookQuery(_context, _mapper) { BookId = id };
            var validator = new GetBookQueryValidator();
            validator.ValidateAndThrow(query);
            return Ok(query.Handle());
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel bookModel)
        {

            var createBookCommand = new CreateBookCommand(_context, _mapper) { BookModel = bookModel };
            var validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(createBookCommand);
            createBookCommand.Handle();
            return Ok();
        }

        [HttpPut("id")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel bookModel)
        {
            var updateBookCommand = new UpdateBookCommand(_context) { BookModel = bookModel, BookId = id };
            var validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(updateBookCommand);
            updateBookCommand.Handle();
            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteBook(int id)
        {
            var deleteBookCommand = new DeleteBookCommand(_context) { BookId = id };
            var validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(deleteBookCommand);
            deleteBookCommand.Handle();
            return Ok();
        }
    }
}