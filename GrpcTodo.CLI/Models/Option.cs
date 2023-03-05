using GrpcTodo.CLI.Enums;

namespace GrpcTodo.CLI.Models;

public sealed class Option
{
    public string? Name { get; set; }
    public required string Path { get; set; }
    public Command? Action { get; set; }
    public required List<Option> Children { get; set; }
}