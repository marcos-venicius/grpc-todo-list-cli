using GrpcTodo.CLI.Lib;
using GrpcTodo.CLI.UseCases.Common;
using GrpcTodo.CLI.Utils;
using GrpcTodo.SharedKernel.Protos.Tasks;
using GrpcTodo.SharedKernel.Protos.Tasks.Requests;

namespace GrpcTodo.CLI.UseCases.TaskDelete;
public class TaskDeleteUseCase : UseCase
{
    public TaskDeleteUseCase(ConfigsManager configsManager) : base(configsManager)
    {
    }

    public override async Task ExecuteAsync()
    {
        var accessToken = _configsManager.GetItem(ConfigKey.Item, Settings.AuthTokenKey);

        if (accessToken is null)
            throw new ArgumentNullException(nameof(accessToken), "you are not authenticated");

        using var channel = GrpcConnection.CreateChannel();

        var client = new TaskItem.TaskItemClient(channel);

        var request = new TaskListRequest
        {
            AccessToken = accessToken
        };

        var response = await client.ListAllAsync(request);

        var tasks = response.Items.OrderByDescending(x => x.CreatedAt);

        var taskDeletePrompt = new TaskDeletePrompt();

        var taskIdToDelete = taskDeletePrompt.PromptTask(tasks);

        await client.DeleteAsync(new TaskDeleteRequest {
            TaskId = taskIdToDelete,
            AccessToken = accessToken
        });

        ConsoleWritter.WriteSuccess($"task [{taskIdToDelete}] deleted successfully");
    }
}
