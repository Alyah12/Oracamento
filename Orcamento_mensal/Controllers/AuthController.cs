using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Orcamento_mensal.Context;
using Orcamento_mensal.Dto;
using Orcamento_mensal.Models;

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
    public IActionResult Login([FromBody] UserDto user)
    {
        if (user.Nome == "admin" && user.Senha == "password")
        {
            var token = _jwtService.GenerateToken("1");
            return Ok(new { Token = token });
        }
        return Unauthorized();
    }
}