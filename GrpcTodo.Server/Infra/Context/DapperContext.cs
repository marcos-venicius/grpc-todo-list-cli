using System.Data;
using GrpcTodo.Server.Infra.Interfaces;
using Npgsql;

namespace GrpcTodo.Server.Infra.Context;

public sealed class DapperContext : IContext
{
    private readonly IConfiguration _configuration;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection CreateConnection() => new NpgsqlConnection(_configuration.GetConnectionString("postgresql"));
}