using GrpcTodo.CLI.Utils;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcTodo.SharedKernel.Protos.User;
using GrpcTodo.SharedKernel.Protos.User.Requests;

namespace GrpcTodo.CLI.UseCases;

public sealed class CreateAccountUseCase
{
    public async Task ExecuteAsync()
    {
        var createAccountPrompt = new CreateAccountPrompt();

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

            ConsoleWritter.WriteSuccess("user created successfully");
        }
        catch (RpcException e)
        {
            ConsoleWritter.WriteError(e.Status.Detail ?? e.Message);
        }
    }
}