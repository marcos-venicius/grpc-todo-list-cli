using GrpcTodo.CLI.Lib;
using GrpcTodo.CLI.UseCases.Common;
using GrpcTodo.CLI.Utils;

namespace GrpcTodo.CLI.UseCases.AccountLogout;

public sealed class AccountLogoutUseCase : UseCase
{
    public AccountLogoutUseCase(ConfigsManager configsManager) : base(configsManager) { }

    public override Task ExecuteAsync()
    {
        try
        {
            _configsManager.RemoveItem(ConfigKey.Item, Settings.AuthTokenKey);

            ConsoleWritter.WriteSuccess("user signed out successfully");
        }
        catch (Exception e)
        {
            var message = $"cannot logout: {e.Message}";

            ConsoleWritter.WriteError(message);
        }

        return Task.CompletedTask;
    }
}