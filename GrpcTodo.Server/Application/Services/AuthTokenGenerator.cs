using GrpcTodo.Server.Domain.Services;

namespace GrpcTodo.Server.Application.Services;

public sealed class AuthTokenGenerator : IAuthTokenGenerator
{
    public Guid Generate()
    {
        return Guid.NewGuid();
    }
}