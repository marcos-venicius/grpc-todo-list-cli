using GrpcTodo.Server.Application.Common;
using GrpcTodo.Server.Domain.Middleware;
using GrpcTodo.Server.Domain.Repositories;

namespace GrpcTodo.Server.Application.Middleware;

public sealed class AuthMiddleware : IAuthMiddleware
{
    private readonly IUserRepository _userRepository;

    public AuthMiddleware(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Authenticate<TRequest>(Credentials credentials, TRequest request, Runner<TRequest, Task> runner) where TRequest : class
    {
        var userOnDatabase = await _userRepository.FindByAccessTokenAsync(credentials.AccessToken);

        if (userOnDatabase is null)
            throw new UnauthorizedException("this account does not exists");

        await runner(userOnDatabase.Id, request);
    }

    public async Task<TResponse> Authenticate<TRequest, TResponse>(Credentials credentials, TRequest request, Runner<TRequest, Task<TResponse>> runner)
        where TRequest : class
        where TResponse : notnull
    {
        var userOnDatabase = await _userRepository.FindByAccessTokenAsync(credentials.AccessToken);

        if (userOnDatabase is null)
            throw new UnauthorizedException("this account does not exists");

        return await runner(userOnDatabase.Id, request);
    }
}
