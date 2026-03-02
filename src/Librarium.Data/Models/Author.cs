namespace Librarium.Data.Models;

public class Author {
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Biography { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();
}