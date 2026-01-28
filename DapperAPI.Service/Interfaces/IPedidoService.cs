using DapperAPI.Service.DTO.Pedido;

namespace DapperAPI.Service.Interfaces;

public interface IPedidoService
{
    Task<int> AddPedidoAsync(AdicionarPedidoRequest pedido);
    Task<PedidoResult> GetPedidoByIdAsync(int id);
    Task<IEnumerable<PedidoResult>> GetAllPedidosAsync();
}
