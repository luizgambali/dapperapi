
using Dapper;
using DapperAPI.Infrastructure.Interfaces;

namespace DapperAPI.Service.Helper;

public static class DatabaseInitializer
{
    public static void Initialize(IDbConnectionFactory factory)
    {
        using var conn = factory.Create();
        conn.Open();

        var sql = @" 
            CREATE TABLE IF NOT EXISTS Clientes (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Nome TEXT NOT NULL,
                Email TEXT NOT NULL
            );

            CREATE TABLE IF NOT EXISTS Produtos (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Nome TEXT NOT NULL,
                Preco REAL NOT NULL CHECK (Preco >= 0)
            );

            CREATE TABLE IF NOT EXISTS Pedidos (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                ClienteId INTEGER NOT NULL,
                DataPedido TEXT NOT NULL,
                FOREIGN KEY (ClienteId) REFERENCES Clientes(Id)
            );

            CREATE TABLE IF NOT EXISTS PedidoItens (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                PedidoId INTEGER NOT NULL,
                ProdutoId INTEGER NOT NULL,
                Quantidade INTEGER NOT NULL,
                FOREIGN KEY (PedidoId) REFERENCES Pedidos(Id),
                FOREIGN KEY (ProdutoId) REFERENCES Produtos(Id)
            );
        ";

        conn.Execute(sql);
    }
}
