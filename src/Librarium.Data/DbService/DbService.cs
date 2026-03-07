using Librarium.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Librarium.Data.DbService;

public class DbService : IDbService {
    private readonly LibrariumDbContext _context;

    public DbService(LibrariumDbContext context){
        _context = context;
    }

    //BOOKS 

    public async Task<List<Book>> GetAllBooksAsync(){
        return await _context.Books
            .AsNoTracking()
            .Select(b => new Book
            {
                Id = b.Id,
                Title = b.Title,
                ISBN = b.ISBN,
                PublicationYear = b.PublicationYear,
                Authors = b.Authors.ToList()
            })
            .ToListAsync();
    }


    public async Task<bool> BookExistsAsync(Guid bookId){
        return await _context.Books
            .AnyAsync(b => b.Id == bookId);
    }

    //MEMBERS

    public async Task<List<Member>> GetAllMembersAsync(){
        return await _context.Members
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> MemberExistsAsync(Guid memberId){
        return await _context.Members
            .AnyAsync(m => m.Id == memberId);
    }


    //LOANS 

    public async Task<Loan> CreateLoanAsync(Guid memberId, Guid bookId, DateTime loanDate) {
        var loan = new Loan
        {
            Id = Guid.NewGuid(),
            MemberId = memberId,
            BookId = bookId,
            LoanDate = loanDate,
            ReturnDate = null,
            Status = LoanStatus.Active // This will allow to set the status on the databse wihtout changing the dto, so the frontend is unaffected.
        };

        _context.Loans.Add(loan);

        await _context.SaveChangesAsync();

        return loan;
    }

    public async Task<List<Loan>> GetLoansForMemberAsync(Guid memberId) {
        return await _context.Loans
            .AsNoTracking()
            .Where(l => l.MemberId == memberId)
            .ToListAsync();
    }
}