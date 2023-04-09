using Grpc.Core;
using Grpc.Net.Client;
using GrpcTodo.CLI.Lib;
using GrpcTodo.CLI.UseCases.Common;
using GrpcTodo.CLI.Utils;
using GrpcTodo.SharedKernel.Protos.Tasks;
using GrpcTodo.SharedKernel.Protos.Tasks.Requests;

namespace GrpcTodo.CLI.UseCases.TaskCreate;

public sealed class TaskCreateUseCase : UseCase
{
    public TaskCreateUseCase(ConfigsManager configsManager)
        : base(configsManager) { }

    public override async Task ExecuteAsync()
    {
        try
        {
            var accessToken = _configsManager.GetItem(ConfigKey.Item, Settings.AuthTokenKey);

            if (accessToken is null)
                throw new ArgumentNullException(nameof(accessToken), "you are not authenticated");

            var createTaskPrompt = new TaskCreatePrompt();

            var prompt = createTaskPrompt.PromptName();
            var name = prompt.Name;

            using var channel = GrpcChannel.ForAddress(Settings.ServerAddress);

            var client = new TaskItem.TaskItemClient(channel);

            var request = new TaskCreateRequest
            {
                AccessToken = accessToken,
                Name = name
            };

            var response = await client.CreateAsync(request);

            ConsoleWritter.WriteSuccess($"Task [{response.Id[..4]}] created successfully");
        }
        catch (RpcException e)
        {
            ConsoleWritter.WriteError(e.Status.Detail ?? e.Message);
        }
        catch (ArgumentNullException e)
        {
            ConsoleWritter.WriteError(e.Message);
        }
    }
}
