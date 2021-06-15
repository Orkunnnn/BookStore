using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        public UpdateBookModel BookModel { get; set; }
        public int BookId { get; set; }
        private readonly BookStoreDbContext _dbContext;

        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book == null)
            {
                throw new InvalidOperationException("Kitap mevcut değil");
            }

            book.Title = BookModel.Title ?? book.Title;
            book.GenreId = BookModel.GenreId != default ? BookModel.GenreId : book.GenreId;
            book.PageCount = BookModel.PageCount != default ? BookModel.PageCount : book.PageCount;
            book.PublishDate = BookModel.PublishDate != default ? BookModel.PublishDate : book.PublishDate;
            _dbContext.SaveChanges();
        }
    }

    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}