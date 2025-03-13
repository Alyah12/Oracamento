using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oracamento_mensal.Context;
using Oracamento_mensal.Models;
using Oracamento_mensal.Operation;

namespace Oracamento_mensal.Controllers;

[ApiController]
[Route("[controller]")]
public class BudgetController : ControllerBase
{
    public readonly AppDbContext _context;

    public BudgetController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult> GetAsync()
    {
        var valorAnual = await _context.OrcamentoDespesas.
            GroupBy(x => x.FonteRecurso).
            Select(x => new
            {
                Ano = x.Key,
                UnidadeGestora = x.Sum(y => y.UnidadeGestora)
            }).ToListAsync();

        return Ok(valorAnual);
    }

    [HttpGet("Soma")]
    public async Task<IActionResult> GetSumAsync()
    {
        var sum = new EntityOperations(_context);

        return Ok(sum);
    }

    [HttpGet("Ano:int, UnidadeGestora:int")]

    public IActionResult Search(int Ano, int UnidadeGestora)
    {
        var search = _context.OrcamentoDespesas.
            FindAsync(Ano, UnidadeGestora);

        if (search == null)
        {
            return NotFound();
        }

        return Ok(search);
    }
}