using GrpcTodo.Server.Domain.Entities;
using GrpcTodo.Server.Domain.Repositories;
using GrpcTodo.Server.Infra.Interfaces;
using GrpcTodo.Server.Infra.Queries;

using Dapper;

namespace GrpcTodo.Server.Application.Repositories;

public sealed class TaskRepository : ITaskRepository
{
    private readonly IContext _context;

    public TaskRepository(IContext context)
    {
        _context = context;
    }

    public Task CompleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task CreateAsync(Guid id, string name, Guid userId)
    {
        using var connection = _context.CreateConnection();

        await connection.ExecuteAsync(TaskQueries.Create, new
        {
            id,
            name,
            userId,
            createdAt = DateTimeOffset.Now
        });
    }

    public async Task<TaskItem?> FindByNameAsync(string name)
    {
        using var connection = _context.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<TaskItem>(TaskQueries.FindByName, new
        {
            name
        });
    }

    public Task UncompleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTaskNameAsync(Guid id, string name)
    {
        throw new NotImplementedException();
    }
}
