using GrpcTodo.CLI.Utils;

namespace GrpcTodo.CLI.UseCases.Common;

public abstract class Prompt
{
    private protected record PromptOptions
    {
        public bool AllowEmpty { get; init; } = false;
        public bool RemoveWhitespaces { get; init; } = false;
        public bool ShouldBeNumber { get; init; } = false;
    }

    private protected string Read(string input, PromptOptions? options = null)
    {
        var allowEmpty = options?.AllowEmpty ?? false;
        var removeWhitespaces = options?.RemoveWhitespaces ?? false;
        var shouldBeNumber = options?.ShouldBeNumber ?? false;

        ConsoleWritter.Write(input);

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

        return data;
    }
}