using DapperAPI.Service.DTO.Produto;
using System.ComponentModel.DataAnnotations;

namespace DapperAPI.Service.DTO.Pedido;

public class PedidoItemResult
{
    public int Id { get; set; }
    public int ProdutoId { get; set; }  
    public string ProdutoNome { get; set; } = string.Empty;
    public int Quantidade { get; set; } = 1;
}
