using Microsoft.EntityFrameworkCore;
using Oracamento_mensal.Models;

namespace Oracamento_mensal.Context;

public class AppDbContext : DbContext
{
    protected AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<OrcamentoDespesa> OrcamentoDespesas { get; set; }
    public DbSet<ProgramacaoFinanceira> ProgramacaoFinanceiraDespesa { get; set; }
}