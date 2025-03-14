namespace Orcamento_mensal.Models;

public class OrcamentoDespesa
{
    public int OrcamentoId { get; set; }
    public int Codigo { get; set; }
    public string Ficha { get; set; }
    public int Ano { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataCriacao { get; set; }
    public int UnidadeGestora { get; set; }
    public string ElencoContaCodigo { get; set; }
    public int FonteRecurso { get; set; }
    public int Numero { get; set; }
}