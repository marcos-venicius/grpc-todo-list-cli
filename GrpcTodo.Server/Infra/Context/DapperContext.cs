using System.Data;
using System.Data.SqlClient;

namespace GrpcTodo.Server.Infra.Context;

public sealed class DapperContext
{
    private readonly IConfiguration _configuration;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(_configuration.GetConnectionString("postgresql"));
    }

    public IDbConnection CreateMasterConnection()
    {
        return new SqlConnection(_configuration.GetConnectionString("master"));
    }
}