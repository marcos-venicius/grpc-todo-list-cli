using GrpcTodo.Server.Domain.Common;

namespace GrpcTodo.Server.Domain.Entities;

public sealed class TaskItem : Entity
{
    public Guid UserId { get; set; }

    public string Name { get; set; } = string.Empty;
    public bool Completed { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}
