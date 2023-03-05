namespace GrpcTodo.Server.Infra.Queries;

public static class UserQueries
{
    public const string FindByName = "SELECT * FROM user WHERE LOWER(username) = @username;";
    public const string Create = "INSERT INTO user (username, password) VALUES (@username, @password)";
    public const string GetCurrSequenceValue = "SELECT currval(pg_get_serial_sequence('user','id'));";
}