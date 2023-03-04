namespace Client.Exceptions;

public class ShowErrorMessageException : ApplicationException
{
    public ShowErrorMessageException(string message) : base(message) { }
}