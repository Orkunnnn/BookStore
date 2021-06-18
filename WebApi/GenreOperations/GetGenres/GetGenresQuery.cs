using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.GenreOperations.GetGenres
{
    public class GetGenresQuery
    {
        private readonly BookStoreDbContext _bookStoreDbContext;
        private readonly IMapper _mapper;

        public GetGenresQuery(BookStoreDbContext bookStoreDbContext, IMapper mapper)
        {
            _bookStoreDbContext = bookStoreDbContext;
            _mapper = mapper;
        }

        public List<GenresViewModel> Handle()
        {
            var genreList = _bookStoreDbContext.Genres.OrderBy(x => x.Id).ToList();
            var genresViewModel = _mapper.Map<List<GenresViewModel>>(genreList);
            return genresViewModel;
        }
    }

    public class GenresViewModel
    {
        public string GenreName { get; set; }
    }
}