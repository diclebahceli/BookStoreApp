using System;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.GenreOperations.Commands;

public class CreateGenreCommand
{
    public CreateGenreModel Model { get; set; }
    public readonly BookStoreDBContext bookStoreDBContext;
    public readonly IMapper mapper;

    public CreateGenreCommand(CreateGenreModel model, IMapper mapper, BookStoreDBContext bookStoreDBContext)
    {
        Model = model;
        this.mapper = mapper;
        this.bookStoreDBContext = bookStoreDBContext;
    }
}

public class CreateGenreModel
{
    public string Name { get; set; }
}
