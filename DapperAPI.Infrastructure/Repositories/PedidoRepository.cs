using Dapper;
using DapperAPI.Domain.Interfaces;
using DapperAPI.Domain.Models;
using DapperAPI.Infrastructure.Interfaces;
using Microsoft.Data.Sqlite;

namespace DapperAPI.Infrastructure.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly IDbConnectionFactory _factory;

    public PedidoRepository(IDbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<int> CreatePedidoAsync(Pedido pedido)
    {
        using var conn = _factory.Create();

        conn.Open();

        var transaction = conn.BeginTransaction();

        try
        {
            var sql = @"INSERT INTO Pedidos (ClienteId, DataPedido) VALUES (@ClienteId, @DataPedido); 
                        SELECT last_insert_rowid();" ;

            var pedidoId = await conn.ExecuteScalarAsync<int>(sql, new { pedido.ClienteId, pedido.DataPedido }, transaction);

            if (pedido.Itens == null || !pedido.Itens.Any())
            {
                throw new Exception("O pedido deve conter pelo menos um item.");
            }

            foreach (var item in pedido.Itens)
            {
                var sqlItem = "INSERT INTO PedidoItens (PedidoId, ProdutoId, Quantidade) VALUES (@PedidoId, @ProdutoId, @Quantidade);";
             
                await conn.ExecuteAsync(sqlItem, new { PedidoId = pedidoId, ProdutoId = item.ProdutoId, item.Quantidade }, transaction);
            }

            transaction.Commit();

            return pedidoId;
        }
        catch(SqliteException ex)
        {
            transaction.Rollback();
            return -1;
        }
    }

    public async Task<IEnumerable<Pedido>> GetAllPedidosAsync(int id = 0)
    {
        using var conn = _factory.Create();

        conn.Open();

        //sql com os joins necessários

        var sql = @"SELECT p.Id, p.DataPedido,
                           c.Id, c.Nome, c.Email,
                           i.Id, i.PedidoId, i.ProdutoId, i.Quantidade,
                           pr.Id, pr.Nome, pr.Preco
                    FROM
                        Pedidos p 
                        JOIN Clientes c on (p.ClienteId = c.Id)
                        JOIN PedidoItens i on (p.Id = i.PedidoId)
                        JOIN Produtos pr on (i.ProdutoId = pr.Id)";

        if (id != 0)
        {
            sql += " WHERE p.Id = @Id";
        }

        var pedidoDictionary = new Dictionary<int, Pedido>(); //dicionário para evitar duplicidade

        //mapeamento multi-mapeamento com Dapper (o ultimo item da sequencia é generic, e define o tipo de retorno)
        var pedidos = await conn.QueryAsync<Pedido, Cliente, PedidoItem, Produto, Pedido>
            (
                sql,
                    //tem que estar na mesma ordem do sql!
                   (pedido, cliente, item, produto) =>
                   {
                       //verifica se o pedido já foi adicionado ao dicionário
                       if (!pedidoDictionary.TryGetValue(pedido.Id, out var pedidoEntry))
                       {
                           pedidoEntry = pedido;
                           pedidoEntry.Cliente = cliente;
                           pedidoEntry.Itens = new List<PedidoItem>();
                           pedidoDictionary.Add(pedidoEntry.Id, pedidoEntry);
                       }

                       if (pedidoEntry.Itens == null)
                           pedidoEntry.Itens = new List<PedidoItem>();

                       item.Produto = produto;
                       pedidoEntry.Itens.Add(item);
                       return pedidoEntry;
                   },
                   new { Id = id }, //parametros; independente de existir o parametro na query, pode ser passado. O Dapper vai ignorar se não existir
                 splitOn: "Id, Id, Id, Id"  //aqui é importante definir os pontos de divisão de cada objeto corretamente
            );

        return pedidoDictionary.Values.ToList();
    }

    public async Task<Pedido> GetPedidoByIdAsync(int id)
    {
        var result = await GetAllPedidosAsync(id);

        return result.FirstOrDefault()!;
    }
}
