using DapperAPI.Service.DTO.Cliente;

namespace DapperAPI.Service.Interfaces;

public interface IClienteService
{
    Task<int> AddClienteAsync(AdicionarClienteRequest cliente);
    Task<ClienteResult> GetClienteByIdAsync(int id);
    Task<IEnumerable<ClienteResult>> GetAllClientesAsync();
}
