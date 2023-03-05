namespace GrpcTodo.SharedKernel.Exceptions;

public sealed class ConflictException : ApplicationException
{
    public ConflictException(string message) : base(message) { }
}