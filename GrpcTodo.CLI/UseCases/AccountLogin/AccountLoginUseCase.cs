using GrpcTodo.CLI.Utils;
using Grpc.Core;
using GrpcTodo.SharedKernel.Protos.User;
using GrpcTodo.SharedKernel.Protos.User.Requests;
using GrpcTodo.CLI.Lib;
using GrpcTodo.CLI.UseCases.Common;

namespace GrpcTodo.CLI.UseCases.AccountLogin;

public sealed class AccountLoginUseCase : UseCase
{
    private readonly Parameters _parameters;

    public AccountLoginUseCase(ConfigsManager configsManager, Parameters parameters) : base(configsManager)
    {
        _parameters = parameters;
    }

    public override async Task ExecuteAsync()
    {
        if (_parameters.Has("--help"))
        {
            var help = Menu.GetCommandHelp(Command.Login);

            ConsoleWritter.Write(help);

            return;
        }

        var LoginAccountPrompt = new AccountLoginPrompt();

        var (username, password) = LoginAccountPrompt.Prompt();

        try
        {
            using var channel = GrpcConnection.CreateChannel();

            var client = new User.UserClient(channel);

            var request = new UserLoginRequest
            {
                Name = username,
                Password = password
            };

            var response = await client.LoginAsync(request);

            _configsManager.SetItem(ConfigKey.Item, Settings.AuthTokenKey, response.Token);

            ConsoleWritter.WriteSuccess("user logged in successfully");
        }
        catch (RpcException e)
        {
            ConsoleWritter.WriteError(e.Status.Detail ?? e.Message);
        }
    }
}