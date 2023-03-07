namespace GrpcTodo.Server.Infra.Queries;

public static class UserQueries
{
    public const string FindByName = @"SELECT * FROM ""user"" WHERE LOWER(username) = @username;";
    public const string Create = @"INSERT INTO ""user"" (username, password) VALUES (@username, @password) RETURNING id;";
}