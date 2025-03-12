using System;
using AutoMapper;
using BookStore.Domain.Entities;
using WebApi.BookOperations.GetBookById;
using WebApi.BookOperations.GetBooks;
using static WebApi.BookOperations.AddBooks.CreateBookCommand;
using static WebApi.GenreOperations.Queries.GetGenres.GetGenresQuery;

namespace WebApi.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateBookModel, Book>();
        CreateMap<Book, BookViewModel>();
        CreateMap<Book, BookModel>();
        CreateMap<Genre, GenresViewModel>();
    }
}
