using GrpcTodo.CLI.Lib;

namespace GrpcTodo.CLI.UseCases.Common;

public abstract class UseCase
{
    private protected readonly ConfigsManager _configsManager;

    public UseCase(ConfigsManager configsManager)
    {
        _configsManager = configsManager;
    }
}