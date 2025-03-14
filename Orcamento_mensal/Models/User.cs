using System.ComponentModel.DataAnnotations;

namespace Orcamento_mensal.Models;

public class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O usuário é um campo obrigatório")]

    public string Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória")]
    [Compare("Password", ErrorMessage = "As senhas não conferem")]
    public string Senha { get; set; }
}