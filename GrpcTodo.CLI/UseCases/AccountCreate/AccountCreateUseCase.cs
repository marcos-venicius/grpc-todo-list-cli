using GrpcTodo.CLI.Utils;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcTodo.SharedKernel.Protos.User;
using GrpcTodo.SharedKernel.Protos.User.Requests;
using GrpcTodo.CLI.Lib;
using GrpcTodo.CLI.UseCases.Common;

namespace GrpcTodo.CLI.UseCases.AccountCreate;

public sealed class AccountCreateUseCase : UseCase
{
    private readonly Parameters _paramaters;

    public AccountCreateUseCase(ConfigsManager configsManager, Parameters parameters) : base(configsManager)
    {
        _paramaters = parameters;
    }

    public override async Task ExecuteAsync()
    {
        if (_paramaters.Has("--help"))
        {
            var help = Menu.GetCommandHelp(Command.CreateAccount);

            ConsoleWritter.Write(help);

            return;
        }

        var createAccountPrompt = new AccountCreatePrompt();

        var (username, password) = createAccountPrompt.Prompt();

        try
        {
            using var channel = GrpcChannel.ForAddress(Settings.ServerAddress);

            var client = new User.UserClient(channel);

            var request = new UserCreateRequest
            {
                Name = username,
                Password = password
            };

            var response = await client.CreateAsync(request);

            _configsManager.SetItem(ConfigKey.Item, Settings.AuthTokenKey, response.Token);

            ConsoleWritter.WriteSuccess("user created successfully");
        }
        catch (RpcException e)
        {
            ConsoleWritter.WriteError(e.Status.Detail ?? e.Message);
        }
    }
}