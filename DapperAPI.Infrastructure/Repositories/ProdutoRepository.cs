using Dapper;
using DapperAPI.Domain.Interfaces;
using DapperAPI.Domain.Models;
using DapperAPI.Infrastructure.Interfaces;

namespace DapperAPI.Infrastructure.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly IDbConnectionFactory _factory;

    public ProdutoRepository(IDbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<int> AddProdutoAsync(Produto produto)
    {
        using var conn = _factory.Create();

        conn.Open();

        var sql = "INSERT INTO Produtos (Nome, Preco) VALUES (@Nome, @Preco); " +
                  "SELECT last_insert_rowid();";

        var produtoId = await conn.ExecuteScalarAsync<int>(sql, new { produto.Nome, produto.Preco});

        return produtoId;
    }

    public async Task<IEnumerable<Produto>> GetAllProdutosAsync()
    {
        using var conn = _factory.Create();

        conn.Open();

        var sql = "SELECT Id, Nome, Preco FROM Produtos;";

        var produtos = await conn.QueryAsync<Produto>(sql);

        return produtos;
    }

    public async Task<Produto> GetProdutoByIdAsync(int id)
    {
        using var conn = _factory.Create();

        conn.Open();

        var sql = "SELECT Id, Nome, Preco FROM Produtos where Id = @Id;";

        var produto = await conn.QuerySingleOrDefaultAsync<Produto>(sql, new { Id = id });

        return produto;
    }
}
