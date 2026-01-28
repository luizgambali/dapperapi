using System.ComponentModel.DataAnnotations;

namespace DapperAPI.Service.DTO.Produto;

public class AdicionarProdutoRequest
{
    [Required(ErrorMessage = "O campo nome é obrigatório")]
    [MaxLength(100, ErrorMessage = "O campo nome deve ter no máximo 100 caracteres")] 
    [MinLength(3, ErrorMessage = "O nome deve ter no mnínimo 3 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O preço é obrigatório")] 
    [Range(0,double.MaxValue, ErrorMessage = "O preço é inválido")]
    public decimal Preco { get; set; } = 0;
}
