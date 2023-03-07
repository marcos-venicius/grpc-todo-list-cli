using Grpc.Core;

namespace GrpcTodo.SharedKernel.Exceptions;

public sealed class InvalidUserPasswordException : RpcException
{
    public InvalidUserPasswordException(string message)
        : base(
            new Status(
                StatusCode.InvalidArgument,
                message
            ),
            message
        )
    {
    }
}