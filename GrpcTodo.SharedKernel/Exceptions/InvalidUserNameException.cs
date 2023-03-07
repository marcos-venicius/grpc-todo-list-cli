using Grpc.Core;

namespace GrpcTodo.SharedKernel.Exceptions;

public sealed class InvalidUserNameException : RpcException
{
    public InvalidUserNameException(string message)
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