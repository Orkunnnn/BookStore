using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBook;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
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
            try
            {
                var query = new GetBookQuery(_context, _mapper) { BookId = id };
                return Ok(query.Handle());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel bookModel)
        {
            try
            {
                var createBookCommand = new CreateBookCommand(_context, _mapper) { BookModel = bookModel };
                createBookCommand.Handle();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("id")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel bookModel)
        {
            try
            {
                var updateBookCommand = new UpdateBookCommand(_context) { BookModel = bookModel, BookId = id };
                updateBookCommand.Handle();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("id")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                var deleteBookCommand = new DeleteBookCommand(_context);
                deleteBookCommand.BookId = id;
                deleteBookCommand.Handle();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}