using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.GenreOperations.GetGenre
{
    public class GetGenreQuery
    {
        private readonly IMapper _mapper;
        private readonly BookStoreDbContext _bookStoreDbContext;
        public int GenreId { get; set; }

        public GetGenreQuery(IMapper mapper, BookStoreDbContext bookStoreDbContext)
        {
            _mapper = mapper;
            _bookStoreDbContext = bookStoreDbContext;
        }

        public GenreViewModel Handle()
        {
            var genre = _bookStoreDbContext.Genres.SingleOrDefault(x => x.Id == GenreId);
            if (genre == null)
            {
                throw new InvalidOperationException("Genre mevcut değil.");
            }

            var genreViewModel = _mapper.Map<GenreViewModel>(genre);
            return genreViewModel;
        }
    }

    public class GenreViewModel
    {
        public string GenreName { get; set; }
    }
}