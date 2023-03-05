using GrpcTodo.Server.Domain.Common;

namespace GrpcTodo.Server.Domain.Entities;

public sealed class User : Entity
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
}