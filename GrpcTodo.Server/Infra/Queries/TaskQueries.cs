namespace GrpcTodo.Server.Infra.Queries;

public static class TaskQueries
{
    public const string Complete = "";
    public const string Create = @"INSERT INTO ""task"" (id, userId, name, createdAt) VALUES (@id, @userId, @name, @createdAt)";
    public const string FindByName = @"SELECT * FROM ""task"" WHERE LOWER(name) = @name;";
    public const string Uncomplete = "";
    public const string UpdateTaskName = "";
}

