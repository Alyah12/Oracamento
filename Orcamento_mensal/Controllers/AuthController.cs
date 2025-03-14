using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Orcamento_mensal.Context;

namespace Orcamento_mensal.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly JwtService _jwtService;

    public AuthController(AppDbContext context, JwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (request.Email == _context.User.Email && request.Password == _context.User.Senha)
        {
            var token = _jwtService.GenerateToken("1");
            return Ok(new { Token = token });
        }
        return Unauthorized();
    }
}