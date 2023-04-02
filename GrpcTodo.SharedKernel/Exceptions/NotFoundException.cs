using Grpc.Core;

namespace GrpcTodo.SharedKernel.Exceptions;

public sealed class NotFoundException : RpcException
{
    public NotFoundException(string message) : base(new Status(
        StatusCode.NotFound,
        message
    ), message)
    { }
}
