using Grpc.Core;

namespace GrpcTodo.SharedKernel.Exceptions;

public sealed class DeniedException : RpcException
{
    public DeniedException(string message) : base(new Status(
        StatusCode.PermissionDenied,
        message
    ), message)
    { }
}