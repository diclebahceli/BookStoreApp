using System;

namespace BookStore.Domain.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public Genre Genre { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishDate { get; set; }
}
