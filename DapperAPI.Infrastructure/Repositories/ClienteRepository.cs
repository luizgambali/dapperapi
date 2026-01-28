using Dapper;
using DapperAPI.Domain.Interfaces;
using DapperAPI.Domain.Models;
using DapperAPI.Infrastructure.Interfaces;

namespace DapperAPI.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly IDbConnectionFactory _factory;

    public ClienteRepository(IDbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<int> AddClienteAsync(Cliente cliente)
    {
        using var conn = _factory.Create();

        conn.Open();

        var sql = "INSERT INTO Clientes (Nome, Email) VALUES (@Nome, @Email); " +
                  "SELECT last_insert_rowid();";

        var clienteId = await conn.ExecuteScalarAsync<int>(sql, new { cliente.Nome, cliente.Email});

        return clienteId;
    }

    public async Task<IEnumerable<Cliente>> GetAllClientesAsync()
    {
        using var conn = _factory.Create();

        conn.Open();

        var sql = "SELECT Id, Nome, Email FROM Clientes;";

        var clientes = await conn.QueryAsync<Cliente>(sql);

        return clientes;
    }

    public async Task<Cliente> GetClienteByIdAsync(int id)
    {
        using var conn = _factory.Create();

        conn.Open();

        var sql = "SELECT Id, Nome, Email FROM Clientes where Id = @Id;";

        var cliente = await conn.QuerySingleOrDefaultAsync<Cliente>(sql, new { Id = id });

        return cliente;
    }
}
