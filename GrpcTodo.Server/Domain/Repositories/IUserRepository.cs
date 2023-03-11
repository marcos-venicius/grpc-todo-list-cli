using GrpcTodo.Server.Domain.Entities;

namespace GrpcTodo.Server.Domain.Repositories;

public interface IUserRepository
{
    public Task<Guid> CreateAsync(string username, string password, Guid token);
    public Task<User?> FindByNameAsync(string username);
}