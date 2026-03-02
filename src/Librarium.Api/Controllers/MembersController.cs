using Librarium.Data.DbService;
using Microsoft.AspNetCore.Mvc;

namespace Librarium.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase {
    private readonly IDbService _dbService;

    public MembersController(IDbService service){
        _dbService = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMembers(){
        var members = await _dbService.GetAllMembersAsync();
        return Ok(members);
    }
}