namespace Librarium.Data.Models;

public class Loan {
    public Guid Id { get; set; }

    public Guid MemberId { get; set; }

    public Guid BookId { get; set; }

    public DateTime LoanDate { get; set; }

    public DateTime? ReturnDate { get; set; }
    public LoanStatus Status { get; set; }

    public Member Member { get; set; } = null!;

    public Book Book { get; set; } = null!;
}

public enum LoanStatus{
    Active = 0,
    Returned = 1,
    Overdue = 2,
    Lost = 3
}