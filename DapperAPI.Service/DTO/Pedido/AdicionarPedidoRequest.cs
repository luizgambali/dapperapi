using System.ComponentModel.DataAnnotations;

namespace DapperAPI.Service.DTO.Pedido;

public class AdicionarPedidoRequest
{
    [Required(ErrorMessage = "A data do pedido é obrigatória")]
    public DateTime DataPedido { get; set; }

    [Required(ErrorMessage = "O ID do cliente é obrigatório")]
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "Os itens do pedido são obrigatórios")]
    public List<AdicionarPedidoItemRequest> Itens { get; set; } = new();
}
