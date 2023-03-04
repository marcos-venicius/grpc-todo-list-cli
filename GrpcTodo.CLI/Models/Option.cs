using GrpcTodo.CLI.Enums;

namespace GrpcTodo.CLI.Models;

public class Option
{
    public required string Name { get; set; }
    public required ActionType Action { get; set; }
}