using GrpcTodo.Server.Application.Common;

namespace GrpcTodo.Server.Domain.Middleware;

public interface IAuthMiddleware
{
    Task Authenticate<TRequest>(Credentials credentials, TRequest request, Runner<TRequest, Task> runner) where TRequest : class;
    Task<TResponse> Authenticate<TRequest, TResponse>(Credentials credentials, TRequest request, Runner<TRequest, Task<TResponse>> runner)
        where TRequest : class
        where TResponse : notnull;
    Task<TResponse> Authenticate<TResponse>(Credentials credentials, Runner<Task<TResponse>> runner)
        where TResponse : notnull;
}
