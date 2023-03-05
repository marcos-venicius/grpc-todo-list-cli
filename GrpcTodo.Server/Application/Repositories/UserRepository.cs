using Dapper;
using GrpcTodo.Server.Domain.Entities;
using GrpcTodo.Server.Domain.Repositories;
using GrpcTodo.Server.Infra.Interfaces;
using GrpcTodo.Server.Infra.Queries;

namespace GrpcTodo.Server.Application.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly IContext _context;

    public UserRepository(IContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateAsync(string username, string password)
    {
        using var connection = _context.CreateConnection();

        await connection.ExecuteAsync(UserQueries.Create, new
        {
            username,
            password
        });

        return await connection.QuerySingleAsync<Guid>(UserQueries.GetCurrSequenceValue);
    }

    public async Task<User?> FindByNameAsync(string username)
    {
        using var connection = _context.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<User>(UserQueries.FindByName, new
        {
            username = username.ToLower()
        });
    }
}