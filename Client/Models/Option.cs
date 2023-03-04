using Client.Enums;

namespace Client.Models;

public class Option
{
    public required string Name { get; set; }
    public required ActionType Action { get; set; }
}