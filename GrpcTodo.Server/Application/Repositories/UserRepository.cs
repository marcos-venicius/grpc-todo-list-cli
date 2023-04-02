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

    public async Task<Guid> CreateAsync(string username, string password, Guid token)
    {
        using var connection = _context.CreateConnection();

        return await connection.ExecuteScalarAsync<Guid>(UserQueries.Create, new
        {
            username,
            password,
            token
        });
    }

    public async Task<User?> FindByAccessTokenAsync(Guid accessToken)
    {
        using var connection = _context.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<User>(UserQueries.FindByAccessToken, new
        {
            accessToken
        });
    }

    public async Task<User?> FindByIdAsync(Guid id)
    {
        using var connection = _context.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<User>(UserQueries.FindById, new
        {
            id
        });
    }

    public async Task<User?> FindByNameAsync(string username)
    {
        using var connection = _context.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<User>(UserQueries.FindByName, new
        {
            username = username.ToLower()
        });
    }

    public async Task UpdateTokenAsync(Guid id, Guid token)
    {
        using var connection = _context.CreateConnection();

        await connection.ExecuteAsync(UserQueries.UpdateToken, new
        {
            id,
            token
        });
    }
}
