using DapperAPI.Service.DTO.Produto;
using System.ComponentModel.DataAnnotations;

namespace DapperAPI.Service.DTO.Pedido;

public class AdicionarPedidoItemRequest
{
    [Required]
    public int ProdutoId { get; set; }    
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser no mínimo 1")]
    public int Quantidade { get; set; } = 1;
}
