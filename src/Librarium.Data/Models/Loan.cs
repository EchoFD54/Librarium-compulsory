namespace Librarium.Data.Models;

public class Loan {
    public Guid Id { get; set; }

    public Guid MemberId { get; set; }

    public Guid BookId { get; set; }

    public DateTime LoanDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public Member Member { get; set; } = null!;

    public Book Book { get; set; } = null!;
}