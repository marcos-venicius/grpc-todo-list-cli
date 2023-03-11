using System.Data;

namespace GrpcTodo.Server.Infra.Interfaces;

public interface IContext
{
    public IDbConnection CreateConnection();
}