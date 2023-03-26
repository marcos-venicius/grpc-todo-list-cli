using GrpcTodo.CLI.Lib;

namespace GrpcTodo.CLI.UseCases.Common;

public abstract class UseCase
{
    private protected readonly ConfigsManager _configsManager;

    public UseCase(ConfigsManager configsManager)
    {
        _configsManager = configsManager;
    }

    public abstract Task ExecuteAsync();

    private protected static Task Void => Task.CompletedTask;
}

public abstract class UseCase<T> where T : notnull
{
    private protected readonly ConfigsManager _configsManager;

    public UseCase(ConfigsManager configsManager)
    {
        _configsManager = configsManager;
    }

    public abstract Task<T> ExecuteAsync();

    private protected static Task Void => Task.CompletedTask;
}