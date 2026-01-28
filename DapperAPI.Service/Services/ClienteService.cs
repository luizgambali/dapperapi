using DapperAPI.Domain.Interfaces;
using DapperAPI.Domain.Models;
using DapperAPI.Service.DTO.Cliente;
using DapperAPI.Service.Interfaces;

namespace DapperAPI.Service.Services;

public class ClienteService : IClienteService
{
    public readonly IClienteRepository _clienteRepository;

    public ClienteService(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<int> AddClienteAsync(AdicionarClienteRequest clienteRequest)
    {
        var cliente = new Cliente
        {
            Nome = clienteRequest.Nome,
            Email = clienteRequest.Email
        };

        var result = await _clienteRepository.AddClienteAsync(cliente);

        return result;
    }

    public async Task<IEnumerable<ClienteResult>> GetAllClientesAsync()
    {

        var result = await _clienteRepository.GetAllClientesAsync();

        if (result == null)
            return new List<ClienteResult>();

        return result.Select(c => new ClienteResult
                    {
                        Id = c.Id,
                        Nome = c.Nome,
                        Email = c.Email
                    });
    }

    public async Task<ClienteResult> GetClienteByIdAsync(int id)
    {
        var result  = await _clienteRepository.GetClienteByIdAsync(id);

        if (result == null)
            return null!;

        return new ClienteResult
        {
            Id = result.Id,
            Nome = result.Nome,
            Email = result.Email
        };
    }
}
