using Microsoft.EntityFrameworkCore;
using Orcamento_mensal.Models;

namespace Orcamento_mensal.Context;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<OrcamentoDespesa> OrcamentoDespesas { get; set; }
    public DbSet<ProgramacaoFinanceira> ProgramacaoFinanceiraDespesa { get; set; }

    public DbSet<User> User { get; set; }
}