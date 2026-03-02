namespace Librarium.Api.Dtos;

public record LoanDto(
    Guid Id,
    Guid MemberId,
    Guid BookId,
    DateTime LoanDate,
    DateTime? ReturnDate
);