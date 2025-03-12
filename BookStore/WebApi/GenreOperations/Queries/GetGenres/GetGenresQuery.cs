using System;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.GenreOperations.Queries.GetGenres;

public class GetGenresQuery
{
    public readonly BookStoreDBContext bookStoreDBContext;
    public readonly IMapper mapper;

    public GetGenresQuery(BookStoreDBContext bookStoreDBContext, IMapper mapper)
    {
        this.bookStoreDBContext = bookStoreDBContext;
        this.mapper = mapper;
    }


    public List<GenresViewModel> Handle()
    {
        var genres = bookStoreDBContext.Genres.OrderBy(x => x.Id);
        List<GenresViewModel> res = mapper.Map<List<GenresViewModel>>(genres);
        return res;
    }


    public class GenresViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
