using Grpc.Core;
using Grpc.Net.Client;
using GrpcTodo.SharedKernel.Protos.User;
using GrpcTodo.SharedKernel.Protos.User.Requests;
using GrpcTodo.CLI.UseCases.Common;
using GrpcTodo.CLI.Lib;
using GrpcTodo.CLI.Utils;

namespace GrpcTodo.CLI.UseCases.AccountTokenUpdate;

public sealed class AccountUpdateTokenUseCase : UseCase
{
    private readonly Parameters _parameters;

    public AccountUpdateTokenUseCase(ConfigsManager configsManager, Parameters parameters) : base(configsManager)
    {
        _parameters = parameters;
    }

    public override async Task ExecuteAsync()
    {
        if (_parameters.Has("--help"))
        {
            var help = Menu.GetCommandHelp(Command.UpdateToken);

            ConsoleWritter.Write(help);

            return;
        }

        var accountTokenUpdatePrompt = new AccountTokenUpdatePrompt();

        var (username, password) = accountTokenUpdatePrompt.Prompt();

        try
        {
            using var channel = GrpcChannel.ForAddress(Settings.ServerAddress);

            var client = new User.UserClient(channel);

            var request = new UserUpdateTokenRequest
            {
                Name = username,
                Password = password
            };

            var response = await client.UpdateTokenAsync(request);

            _configsManager.SetItem(ConfigKey.Item, Settings.AuthTokenKey, response.Token);

            ConsoleWritter.WriteSuccess("user auth token update sucessfully");
        }
        catch (RpcException e)
        {
            ConsoleWritter.WriteError(e.Status.Detail ?? e.Message);
        }
    }
}
