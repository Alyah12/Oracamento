using Oracamento_mensal.Context;
using Oracamento_mensal.Models;

namespace Oracamento_mensal.Operation;

public class EntityOperations
{
    public AppDbContext _context { get; set; }

    public EntityOperations(AppDbContext context)
    {
        _context = context;
    }

    public decimal Sum()
    {
        decimal somaTotal = 0;

        // Obtém todos os registros da entidade
        var registros = _context.ProgramacaoFinanceiraDespesa.ToList();

        foreach (var registro in registros)
        {
            // Percorre as propriedades da entidade
            foreach (var prop in typeof(ProgramacaoFinanceiraDespesaConfig).GetProperties())
            {
                // Verifica se a propriedade é numérica (int, decimal, double, float)
                if (prop.PropertyType == typeof(int) ||
                    prop.PropertyType == typeof(decimal) ||
                    prop.PropertyType == typeof(double) ||
                    prop.PropertyType == typeof(float))
                {
                    // Obtém o valor da propriedade e soma
                    var valor = prop.GetValue(registro);
                    if (valor != null)
                    {
                        somaTotal += Convert.ToDecimal(valor);
                    }
                }
            }
        }

        return somaTotal;
    }
}