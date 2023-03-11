using Grpc.Core;

namespace GrpcTodo.SharedKernel.Exceptions;

public sealed class UnauthorizedException : RpcException
{
    public UnauthorizedException(string message) : base(new Status(
        StatusCode.Unauthenticated,
        message
    ), message)
    { }
}