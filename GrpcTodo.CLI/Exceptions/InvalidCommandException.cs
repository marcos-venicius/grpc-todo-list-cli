namespace GrpcTodo.CLI.Exceptions;

public sealed class InvalidCommandException : ApplicationException
{
    public InvalidCommandException(string message) : base(message) { }
}