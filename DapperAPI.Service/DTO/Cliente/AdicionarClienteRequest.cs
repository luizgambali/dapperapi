using System.ComponentModel.DataAnnotations;

namespace DapperAPI.Service.DTO.Cliente;

public class AdicionarClienteRequest
{
    [Required(ErrorMessage = "Nome do cliente é obrigatório")]
    [MinLength(3, ErrorMessage = "O nome do cliente deve conter no mínimo 3 caracteres")]
    [MaxLength(100, ErrorMessage = "O nome do cliente deve conter no máximo 100 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O e-mail do cliente é obrigatório")]
    [EmailAddress(ErrorMessage = "E-mail inválido")]
    public string Email { get; set; } = string.Empty;
}
