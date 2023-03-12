namespace GrpcTodo.Server.Domain.Services;

public interface IAuthTokenGenerator
{
    public Guid Generate();
}