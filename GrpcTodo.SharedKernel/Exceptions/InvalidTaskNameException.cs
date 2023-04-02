using Grpc.Core;

namespace GrpcTodo.SharedKernel.Exceptions;

public sealed class InvalidTaskNameException : RpcException
{
    public InvalidTaskNameException(string message) : base(new Status(StatusCode.Unknown, message), message) {}
}
