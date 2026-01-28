using System.Data;

namespace DapperAPI.Infrastructure.Interfaces;

public interface IDbConnectionFactory
{       
    IDbConnection Create();
}
