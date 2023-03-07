using GrpcTodo.CLI.Enums;

namespace GrpcTodo.CLI.Models;

public sealed class MenuOption
{
    public string? Description { get; set; }
    public Command? Command { get; set; }

    public required string Path { get; set; }
    public required List<MenuOption> Children { get; set; }
    public bool IsImplemented { get; set; }

    public override string ToString()
    {
        return $"{{ description: {Description}, path: {Path}, command: {Command}, children: [{Children.Count}]}}";
    }
}