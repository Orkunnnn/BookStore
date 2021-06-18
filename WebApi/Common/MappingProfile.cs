using AutoMapper;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBook;
using WebApi.BookOperations.GetBooks;
using WebApi.GenreOperations.CreateGenre;
using WebApi.GenreOperations.GetGenres;
using WebApi.GenreOperations.UpdateGenre;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<CreateGenreViewModel, Genre>();
            CreateMap<Book, BookViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
            CreateMap<Genre, GenresViewModel>();
            CreateMap<UpdateGenreViewModel, Genre>();
        }
    }
}