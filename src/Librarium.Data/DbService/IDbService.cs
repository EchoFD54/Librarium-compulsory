using Librarium.Data.Models;

namespace Librarium.Data.DbService;

public interface IDbService {
    // BOOKS
    Task<List<Book>> GetAllBooksAsync();
      Task<bool> BookExistsAsync(Guid bookId);

    // MEMBERS
    Task<List<Member>> GetAllMembersAsync();
    Task<bool> MemberExistsAsync(Guid memberId);

    // LOANS
    Task<Loan> CreateLoanAsync(
        Guid memberId,
        Guid bookId,
        DateTime loanDate);

    Task<List<Loan>> GetLoansForMemberAsync(Guid memberId);
}