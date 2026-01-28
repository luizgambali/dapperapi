namespace DapperAPI.Domain.Models;

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }

    public IEnumerable<PedidoItem>? PedidoItems { get; set; }
}
