using GrpcTodo.Server.Domain.Entities;

namespace GrpcTodo.Server.Domain.Repositories;

public interface ITaskRepository
{
    public Task CreateAsync(Guid id, string name, Guid userId);
    public Task<TaskItem?> FindByNameAsync(string name);
    public Task UpdateTaskNameAsync(Guid id, string name);
    public Task CompleteAsync(Guid id);
    public Task UncompleteAsync(Guid id);
    public Task<List<TaskItem>> ListAsync(Guid userId);
}
