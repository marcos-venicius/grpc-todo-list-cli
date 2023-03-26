using GrpcTodo.CLI.UseCases.Common;
using GrpcTodo.CLI.Utils;

namespace GrpcTodo.CLI.UseCases.AliasCreate;

public sealed class AliasCreatePrompt : Prompt
{
    public string PromptAlias()
    {
        return Read("alias name: ", new PromptOptions
        {
            RemoveWhitespaces = true,
            ShouldBeSingleWord = true
        });
    }

    public Command PromptCommand(List<(string path, Models.MenuOption option)> commandsWithPaths, Dictionary<string, bool> commandsWithALias)
    {
        ConsoleWritter.Write("\nChose a command to create an alias:\n\n");

        if (commandsWithALias.Any())
            ConsoleWritter.WriteWithColor("* the yellow ones, already have an alias\n\n", ConsoleColor.DarkYellow);

        for (int i = 0; i < commandsWithPaths.Count; i++)
        {
            var commandWithPath = commandsWithPaths[i];

            var index = (i + 1).ToString().PadLeft(2, '0');

            if (commandsWithALias.ContainsKey(commandWithPath.path))
                ConsoleWritter.WriteWithColor($"[XX] {commandWithPath.path,-50}  {commandWithPath.option.Description,-100}", ConsoleColor.DarkYellow);
            else
                ConsoleWritter.WriteWithColor($"[{index}] {commandWithPath.path,-50}  {commandWithPath.option.Description,-100}", ConsoleColor.Green);
        }

        var commandIndex = int.Parse(Read("command id: ", new PromptOptions
        {
            RemoveWhitespaces = true,
            ShouldBeNumber = true
        }));

        if (commandIndex > commandsWithPaths.Count || commandIndex < 1)
        {
            ConsoleWritter.WriteError("invalid option");

            return PromptCommand(commandsWithPaths, commandsWithALias);
        }

        var option = commandsWithPaths[commandIndex - 1];

        if (commandsWithALias.ContainsKey(option.path))
        {
            ConsoleWritter.WriteError("invalid option");

            return PromptCommand(commandsWithPaths, commandsWithALias);
        }

        return (Command)commandsWithPaths[commandIndex - 1].option.Command!;
    }
}