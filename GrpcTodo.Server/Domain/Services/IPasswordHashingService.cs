namespace GrpcTodo.Server.Domain.Services;

public interface IPasswordHashingService
{
    public string Hash(string password);
}