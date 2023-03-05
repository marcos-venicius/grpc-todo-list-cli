using GrpcTodo.CLI.Enums;
using GrpcTodo.CLI.Models;

namespace GrpcTodo.CLI.Services;

public sealed class CommandReader
{
    private readonly string[] _args;
    private readonly List<Option> _options;

    public CommandReader(List<Option> options, string[] args)
    {
        _options = options;
        _args = args;
    }

    public Command? Read()
    {
        var rootOptions = _options;
        var currentArgPos = 0;
        var currentOptionPos = 0;

        while (currentOptionPos < rootOptions.Count)
        {
            var option = rootOptions[currentOptionPos];

            if (option.Path == _args[currentArgPos])
            {
                if (currentArgPos == _args.Length - 1)
                    return option.Action;

                rootOptions = option.Children;
                currentOptionPos = 0;
                currentArgPos++;
            }
            else
                currentOptionPos++;
        }

        return null;
    }

    public override string ToString()
    {
        return string.Join(' ', _args);
    }
}