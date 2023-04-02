namespace GrpcTodo.Server.Application.Common;

public sealed class Credentials
{
    public Guid AccessToken { get; }

    public Credentials(string accessToken)
    {
        AccessToken = Guid.Parse(accessToken);
    }

    public Credentials(Guid accessToken)
    {
        AccessToken = accessToken;
    }
}
