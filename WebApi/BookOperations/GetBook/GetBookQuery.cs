namespace WebApi.BookOperations.GetBook
{
    using System;
    using System.Linq;
    using AutoMapper;
    using FluentValidation;
    using DbOperations;

    public class GetBookQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBookQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public int BookId { get; set; }

        public BookViewModel Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);

            if (book == null)
            {
                throw new InvalidOperationException("Kitap mevcut değil");
            }

            var bookViewModel = _mapper.Map<BookViewModel>(book);
            return bookViewModel;
        }
    }

    public class BookViewModel
    {
        public string Title { get; set; }

        public string Genre { get; set; }

        public int PageCount { get; set; }

        public string PublishDate { get; set; }
    }
}