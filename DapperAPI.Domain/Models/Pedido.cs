namespace DapperAPI.Domain.Models;

public class Pedido
{
    public int Id { get; set; }
    public DateTime DataPedido { get; set; }
    public int ClienteId { get; set; }

    public Cliente? Cliente { get; set; } 
    public List<PedidoItem>? Itens { get; set; } = new List<PedidoItem>();
}
