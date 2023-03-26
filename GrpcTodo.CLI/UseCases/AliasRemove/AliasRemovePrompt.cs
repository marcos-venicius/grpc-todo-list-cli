using GrpcTodo.CLI.UseCases.Common;
using GrpcTodo.CLI.Utils;

namespace GrpcTodo.CLI.UseCases.AliasRemove;

public sealed class AliasRemovePrompt : Prompt
{
    public string PromptAlias(Dictionary<string, string> aliases)
    {
        var keys = aliases.Keys.ToArray();

        ConsoleWritter.WriteInfo("Choose the alias to remove: ");

        Console.WriteLine();

        for (var i = 0; i < keys.Length; i++)
        {
            var key = keys[i];

            var index = (i + 1).ToString().PadLeft(2, '0');

            ConsoleWritter.WriteWithColor($"[{index}]  {key,-30}{aliases[key],-30}", ConsoleColor.Green);
        }

        Console.WriteLine();


        var aliasIndex = int.Parse(Read("alias: ", new PromptOptions
        {
            RemoveWhitespaces = true,
            ShouldBeNumber = true,
            CustomMessage = "Invalid alias index",
            Custom = (data) =>
            {
                var i = int.Parse(data);

                return i < 1 || i > keys.Length;
            }
        }));

        return keys[aliasIndex - 1];
    }
}