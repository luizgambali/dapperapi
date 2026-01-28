using DapperAPI.Domain.Models;

namespace DapperAPI.Domain.Interfaces;

public interface IPedidoRepository
{
    Task<IEnumerable<Pedido>> GetAllPedidosAsync(int id = 0);
    Task<Pedido> GetPedidoByIdAsync(int id);
    Task<int> CreatePedidoAsync(Pedido pedido);    
}
