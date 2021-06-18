using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.GenreOperations.UpdateGenre
{
    public class UpdateGenreCommand
    {
        private readonly BookStoreDbContext _bookStoreDbContext;
        private readonly IMapper _mapper;
        public UpdateGenreViewModel GenreViewModel { get; set; }
        public int GenreId { get; set; }

        public UpdateGenreCommand(BookStoreDbContext bookStoreDbContext, IMapper mapper)
        {
            _bookStoreDbContext = bookStoreDbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var genre = _bookStoreDbContext.Genres.SingleOrDefault(x => x.Id == GenreId);
            if (genre == null)
            {
                throw new InvalidOperationException("Güncellenecek genre bulunamadı.");
            }

            genre = _mapper.Map<Genre>(GenreViewModel);
            _bookStoreDbContext.SaveChanges();
        }
    }

    public class UpdateGenreViewModel
    {
        public string GenreName { get; set; }
    }
}