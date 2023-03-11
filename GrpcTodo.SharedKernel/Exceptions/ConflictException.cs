using Grpc.Core;

namespace GrpcTodo.SharedKernel.Exceptions;

public sealed class ConflictException : RpcException
{
    public ConflictException(string message) : base(new Status(
        StatusCode.Unknown,
        message
    ), message)
    { }
}