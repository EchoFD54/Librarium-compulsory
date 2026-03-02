
using Librarium.Api.Dtos;
using Librarium.Data.DbService;
using Microsoft.AspNetCore.Mvc;

namespace Librarium.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoansController : ControllerBase {
    private readonly IDbService _dbService;

    public LoansController(IDbService service){
        _dbService = service;
    }

    public record CreateLoanRequest(
    Guid MemberId,
    Guid BookId,
    DateTime LoanDate
);

    [HttpPost]
    public async Task<ActionResult<LoanDto>> CreateLoan(CreateLoanRequest request){
        if (!await _dbService.MemberExistsAsync(
                request.MemberId))
            return NotFound("Member not found");

        if (!await _dbService.BookExistsAsync(
                request.BookId))
            return NotFound("Book not found");


        var loan = await _dbService.CreateLoanAsync(
            request.MemberId,
            request.BookId,
            request.LoanDate);

        return Ok(new LoanDto(
            loan.Id,
            loan.MemberId,
            loan.BookId,
            loan.LoanDate,
            loan.ReturnDate));
    }


    [HttpGet("{memberId:guid}")]
    public async Task<ActionResult<IEnumerable<LoanDto>>> GetLoansForMember(Guid memberId) {
        if (!await _dbService.MemberExistsAsync(memberId))
            return NotFound();

        var loans =
            await _dbService.GetLoansForMemberAsync(
                memberId);

        var result = loans.Select(l => new LoanDto(
                l.Id,
                l.MemberId,
                l.BookId,
                l.LoanDate,
                l.ReturnDate));

        return Ok(result);
    }
}