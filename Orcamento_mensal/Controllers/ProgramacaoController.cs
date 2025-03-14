using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orcamento_mensal.Context;
using Orcamento_mensal.Models;

namespace Orcamento_mensal.Controllers;

[ApiController]
[Route("[controller]")]
public class ProgramacaoController : ControllerBase
{
    public readonly AppDbContext _context;

    public ProgramacaoController(AppDbContext context)
    {
        _context = context;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> PostAsync(ProgramacaoFinanceira programacaoFinanceira)
    {
        _context.ProgramacaoFinanceiraDespesa.Add(programacaoFinanceira);
        await _context.SaveChangesAsync();

        return Ok("Produto adicionado com sucesso");
    }

    [HttpPut]
    public async Task<ActionResult> PutAsync (int Id, [FromBody] ProgramacaoFinanceira programacao)
    {
        if (Id != programacao.ProgramacaoId)
        {
            return BadRequest("Houve um erro na requisição");
        }

        _context.Entry(programacao).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("calcular/{ano}/{unidadeGestoraId}")]
    public async Task<IActionResult> CalcularProgressaoAsync(int ano, int unidadeGestoraId)
    {
        var orcamento = await _context.OrcamentoDespesas
            .FirstOrDefaultAsync(o => o.Ano == ano && o.UnidadeGestora == unidadeGestoraId);

        if (orcamento == null)
            return NotFound("Orçamento não encontrado.");

        var programacao = await _context.ProgramacaoFinanceiraDespesa
            .FirstOrDefaultAsync(p => p.Ano == ano);

        decimal valorTotalAnual = orcamento.Valor;
        decimal[] valoresMensais = new decimal[12];

        if (programacao != null)
        {
            decimal[] percentuais =
            {
                programacao.Mes01, programacao.Mes02, programacao.Mes03, programacao.Mes04,
                programacao.Mes05, programacao.Mes06, programacao.Mes07, programacao.Mes08,
                programacao.Mes09, programacao.Mes10, programacao.Mes11, programacao.Mes12
            };

            decimal somaPercentuais = percentuais.Sum();
            decimal totalCalculado = 0;
            decimal sobra = 0;

            for (int i = 0; i < 12; i++)
            {
                valoresMensais[i] = Math.Round((valorTotalAnual * percentuais[i]) / somaPercentuais, 2);
                totalCalculado += valoresMensais[i];
            }

            sobra = valorTotalAnual - totalCalculado;

            if (sobra != 0)
            {
                valoresMensais[11] += sobra;
            }
        }
        else
        {
            for (int i = 0; i < 12; i++)
            {
                valoresMensais[i] = Math.Round(valorTotalAnual / 12, 2);
            }
        }

        return Ok(new { Ano = ano, UnidadeGestoraIDFK = unidadeGestoraId, ValoresMensais = valoresMensais });
    }


    [HttpGet("Ano:int, UnidadeGestora:int")]

    public IActionResult SearchAsync(int Ano, int UnidadeGestora)
    {
        var search = _context.OrcamentoDespesas.
            FindAsync(Ano, UnidadeGestora);

        if (search == null)
        {
            return NotFound();
        }

        return Ok(search);
    }

    [Authorize]
    [HttpDelete]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        if (id == null)
        {
            return BadRequest();
        }

        var programacao = await _context.ProgramacaoFinanceiraDespesa.FindAsync(id);
        _context.ProgramacaoFinanceiraDespesa.Remove(programacao);
        await _context.SaveChangesAsync();

        return Ok();
    }
}