namespace Orcamento_mensal.Dto;

public record UserDto
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
}