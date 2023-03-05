namespace GrpcTodo.SharedKernel.Exceptions;

public sealed class InvalidUserNameException : ApplicationException
{
    public InvalidUserNameException(string message) : base(message)
    {
    }
}