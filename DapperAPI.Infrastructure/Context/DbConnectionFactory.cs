using DapperAPI.Infrastructure.Interfaces;
using Microsoft.Data.Sqlite;
using System.Data;

namespace DapperAPI.Infrastructure.Context;

public class DbConnectionFactory: IDbConnectionFactory
{
    private readonly string _connectionString;

    public DbConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection Create()
    {
        return new SqliteConnection(_connectionString);
    }
}
