using FluentValidation;

namespace WebApi.Controllers
{
    using System;

    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;

    using BookOperations.CreateBook;
    using BookOperations.DeleteBook;
    using BookOperations.GetBook;
    using BookOperations.GetBooks;
    using BookOperations.UpdateBook;
    using DbOperations;

    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        private readonly IMapper _mapper;

        public BooksController(BookStoreDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            var query = new GetBooksQuery(this._context, this._mapper);
            return this.Ok(query.Handle());
        }

        [HttpGet("id")]
        public IActionResult GetBookById(int id)
        {
            try
            {
                var query = new GetBookQuery(this._context, this._mapper) { BookId = id };
                var validator = new GetBookQueryValidator();
                validator.ValidateAndThrow(query);
                return this.Ok(query.Handle());
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel bookModel)
        {
            try
            {
                var createBookCommand = new CreateBookCommand(this._context, this._mapper) { BookModel = bookModel };
                var validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(createBookCommand);
                createBookCommand.Handle();
                return this.Ok();
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [HttpPut("id")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel bookModel)
        {
            try
            {
                var updateBookCommand = new UpdateBookCommand(this._context) { BookModel = bookModel, BookId = id };
                var validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(updateBookCommand);
                updateBookCommand.Handle();
                return this.Ok();
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [HttpDelete("id")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                var deleteBookCommand = new DeleteBookCommand(this._context) { BookId = id };
                var validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(deleteBookCommand);
                deleteBookCommand.Handle();
                return this.Ok();
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
    }
}