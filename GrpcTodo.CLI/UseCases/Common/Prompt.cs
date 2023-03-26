using GrpcTodo.CLI.Utils;

namespace GrpcTodo.CLI.UseCases.Common;

public abstract class Prompt
{
    private protected record PromptOptions
    {
        public bool AllowEmpty { get; init; } = false;
        public bool RemoveWhitespaces { get; init; } = false;
        public bool ShouldBeNumber { get; init; } = false;
        public bool ShouldBeSingleWord { get; init; } = false;
    }

    private protected string Read(string input, PromptOptions? options = null)
    {
        var allowEmpty = options?.AllowEmpty ?? false;
        var removeWhitespaces = options?.RemoveWhitespaces ?? false;
        var shouldBeNumber = options?.ShouldBeNumber ?? false;
        var shouldBeSingleWord = options?.ShouldBeSingleWord ?? false;

        ConsoleWritter.Write(input, true);

        var data = Console.ReadLine();

        data ??= "";

        if (!allowEmpty)
        {
            if (!removeWhitespaces && data.Length == 0)
            {
                ConsoleWritter.WriteError("cannot be empty");

                return Read(input, options);
            }
            else if (removeWhitespaces && data.Trim().Length == 0)
            {
                ConsoleWritter.WriteError("cannot be empty");

                return Read(input, options);
            }
        }

        if (removeWhitespaces)
            data = data.Trim();

        if (shouldBeNumber && !int.TryParse(data, out var _))
        {
            ConsoleWritter.WriteError("should be a valid number");

            return Read(input, options);
        }

        if (shouldBeSingleWord && data.Split(" ").Length > 1)
        {
            ConsoleWritter.WriteError("should be a single word");

            return Read(input, options);
        }

        return data;
    }
}