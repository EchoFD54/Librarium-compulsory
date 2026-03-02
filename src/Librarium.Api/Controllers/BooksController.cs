using Librarium.Data.DbService;
using Microsoft.AspNetCore.Mvc;

namespace Librarium.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase {
    private readonly IDbService _dbService;

    public BooksController(IDbService service){
        _dbService = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks(){
        var books = await _dbService.GetAllBooksAsync();
        return Ok(books);
    }
}