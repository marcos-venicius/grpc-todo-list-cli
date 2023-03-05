using GrpcTodo.CLI.Enums;
using GrpcTodo.CLI.Models;
using GrpcTodo.CLI.Utils;

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

    private static int ComputeLevenshteinDistance(string source, string target)
    {
        if ((source == null) || (target == null)) return 0;
        if ((source.Length == 0) || (target.Length == 0)) return 0;
        if (source == target) return source.Length;

        int sourceWordCount = source.Length;
        int targetWordCount = target.Length;

        if (sourceWordCount == 0)
            return targetWordCount;

        if (targetWordCount == 0)
            return sourceWordCount;

        int[,] distance = new int[sourceWordCount + 1, targetWordCount + 1];

        for (int i = 0; i <= sourceWordCount; distance[i, 0] = i++) ;
        for (int j = 0; j <= targetWordCount; distance[0, j] = j++) ;

        for (int i = 1; i <= sourceWordCount; i++)
        {
            for (int j = 1; j <= targetWordCount; j++)
            {
                int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;

                distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1), distance[i - 1, j - 1] + cost);
            }
        }

        return distance[sourceWordCount, targetWordCount];
    }

    private static List<string> GetPaths(Option option)
    {
        List<string> paths = new();

        void T(List<Option> options, string path = "")
        {
            if (option.Path != path)
                paths.Add(path);

            foreach (var option in options)
            {
                var _path = $"{path} {option.Path}";

                T(option.Children, _path);
            }
        }

        T(option.Children, option.Path);

        return paths;
    }

    public void GetNearestCommand(string command)
    {
        List<string> paths = new();

        foreach (var option in _options)
        {
            var optionPaths = GetPaths(option);

            paths.AddRange(optionPaths);
        }

        int currentLevenshteinDistance = ComputeLevenshteinDistance(command, paths[0]);
        string currentPath = paths[0];

        for (int i = 1; i < paths.Count; i++)
        {
            var levenshteinDistance = ComputeLevenshteinDistance(command, paths[i]);

            if (levenshteinDistance < currentLevenshteinDistance)
            {
                currentLevenshteinDistance = levenshteinDistance;
                currentPath = paths[i];
            }
        }

        ConsoleWritter.WriteSuccess($@"did you mean ""{currentPath}""?", "tip");
    }

    private static void ShowOnDisplayRecursively(List<Option> options, int tabs)
    {
        foreach (var option in options)
        {
            string tab = new string(' ', tabs);

            string message = $"{tab}- {option.Path}";

            if (tabs == 0)
            {
                Console.WriteLine();
            }

            ConsoleWritter.WriteInfo(message);

            if (option.Children.Any())
                ShowOnDisplayRecursively(option.Children, tabs + 4);
        }
    }

    public void ShowAvailableCommands()
    {
        ConsoleWritter.Write("AVAILABLE COMMANDS:");

        ShowOnDisplayRecursively(_options, 0);

        Console.WriteLine();
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