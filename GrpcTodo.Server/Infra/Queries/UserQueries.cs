namespace GrpcTodo.Server.Infra.Queries;

public static class UserQueries
{
    public const string UpdateToken = @"UPDATE ""user"" SET ""Token"" = @token WHERE ""id"" = @id;";
    public const string FindByName = @"SELECT * FROM ""user"" WHERE LOWER(username) = @username;";
    public const string Create = @"INSERT INTO ""user"" (username, password, token) VALUES (@username, @password, @token) RETURNING id;";
}