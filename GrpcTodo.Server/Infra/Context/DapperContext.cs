using System.Data;
using System.Data.SqlClient;
using GrpcTodo.Server.Infra.Interfaces;

namespace GrpcTodo.Server.Infra.Context;

public sealed class DapperContext : IContext
{
    private readonly IConfiguration _configuration;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection CreateConnection() => new SqlConnection(_configuration.GetConnectionString("postgresql"));
}