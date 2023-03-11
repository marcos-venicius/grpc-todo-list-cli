using GrpcTodo.CLI.Lib;
using GrpcTodo.CLI.UseCases.Common;
using GrpcTodo.CLI.Utils;

namespace GrpcTodo.CLI.UseCases;

public sealed class AccountLogoutUseCase : UseCase
{
    public AccountLogoutUseCase(ConfigsManager configsManager) : base(configsManager) { }

    public void Execute()
    {
        try
        {
            _configsManager.Remove(Settings.AuthTokenKey);

            ConsoleWritter.WriteSuccess("user signed out successfully");
        }
        catch (Exception e)
        {
            var message = $"cannot logout: {e.Message}";

            ConsoleWritter.WriteError(message);
        }
    }
}