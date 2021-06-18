using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.GenreOperations.CreateGenre
{
    public class CreateGenreCommand
    {
        private readonly BookStoreDbContext _bookStoreDbContext;
        private readonly IMapper _mapper;
        public CreateGenreViewModel GenreModel { get; set; }

        public CreateGenreCommand(BookStoreDbContext bookStoreDbContext, IMapper mapper)
        {
            _bookStoreDbContext = bookStoreDbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var genre = _bookStoreDbContext.Genres.SingleOrDefault(x => x.Name == GenreModel.GenreName);
            if (genre != null)
            {
                throw new InvalidOperationException("Genre zaten mevcut");
            }

            genre = _mapper.Map<Genre>(GenreModel);
            _bookStoreDbContext.Genres.Add(genre);
            _bookStoreDbContext.SaveChanges();
        }
    }

    public class CreateGenreViewModel
    {
        public string GenreName { get; set; }
    }
}