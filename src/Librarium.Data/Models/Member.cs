namespace Librarium.Data.Models;

public class Member {
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; } 

    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}