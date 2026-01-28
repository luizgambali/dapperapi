namespace DapperAPI.Service.DTO.Pedido;

public class PedidoResult
{
    public int Id { get; set; }    
    public int ClienteId { get; set; }    
    public string ClienteNome { get; set; }
    public DateTime DataPedido { get; set; }    
    public List<PedidoItemResult> Itens { get; set; } = new List<PedidoItemResult>();
}
