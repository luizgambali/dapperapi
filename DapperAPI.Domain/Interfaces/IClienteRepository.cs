using DapperAPI.Domain.Models;

namespace DapperAPI.Domain.Interfaces;

public interface IClienteRepository
{
    Task<int> AddClienteAsync(Cliente cliente);
    Task<Cliente> GetClienteByIdAsync(int id);
    Task<IEnumerable<Cliente>> GetAllClientesAsync();
}
