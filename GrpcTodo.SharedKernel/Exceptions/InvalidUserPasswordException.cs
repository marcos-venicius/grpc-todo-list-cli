namespace GrpcTodo.SharedKernel.Exceptions;

public sealed class InvalidUserPasswordException : ApplicationException
{
    public InvalidUserPasswordException(string message) : base(message)
    {
    }
}