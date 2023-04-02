using GrpcTodo.Server.Domain.Services;

namespace GrpcTodo.Server.Application.Services;

public sealed class GuidGenerator : IGuidGenerator
{
    public Guid Generate() => Guid.NewGuid();
}
