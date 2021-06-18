using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.GenreOperations.DeleteGenre
{
    public class DeleteGenreCommand
    {
        private readonly BookStoreDbContext _bookStoreDbContext;
        public int GenreId { get; set; }

        public DeleteGenreCommand(BookStoreDbContext bookStoreDbContext)
        {
            _bookStoreDbContext = bookStoreDbContext;
        }

        public void Handle()
        {
            var genre = _bookStoreDbContext.Genres.SingleOrDefault(x => x.Id == GenreId);
            if (genre == null)
            {
                throw new InvalidOperationException("Silinecek genre mevcut değil");
            }

            _bookStoreDbContext.Genres.Remove(genre);
            _bookStoreDbContext.SaveChanges();
        }
    }
}