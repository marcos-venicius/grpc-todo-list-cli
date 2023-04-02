namespace GrpcTodo.Server.Infra.Queries;

public static class TaskQueries
{
    public const string Complete = "";
    public const string Create = """INSERT INTO "task" (id, user_id, name, created_at) VALUES (@id, @userId, @name, @createdAt)""";
    public const string FindByName = @"SELECT id, user_id as userId, name, completed, created_at as CreatedAt FROM ""task"" WHERE LOWER(name) = @name;";
    public const string Uncomplete = "";
    public const string UpdateTaskName = "";
}

