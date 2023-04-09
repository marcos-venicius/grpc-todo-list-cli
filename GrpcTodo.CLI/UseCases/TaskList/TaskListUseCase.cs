using System.Text;

using Grpc.Core;
using Grpc.Net.Client;

using GrpcTodo.CLI.Lib;
using GrpcTodo.CLI.UseCases.Common;
using GrpcTodo.CLI.Utils;
using GrpcTodo.SharedKernel.Protos.Tasks;
using GrpcTodo.SharedKernel.Protos.Tasks.Requests;

namespace GrpcTodo.CLI.UseCases.TaskList;

public class TaskListUseCase : UseCase
{
    private readonly Parameters _parameters;

    public TaskListUseCase(ConfigsManager configsManager, Parameters parameters) : base(configsManager)
    {
        _parameters = parameters;
    }

    public override async Task ExecuteAsync()
    {
        try
        {
            var accessToken = _configsManager.GetItem(ConfigKey.Item, Settings.AuthTokenKey);

            if (accessToken is null)
                throw new ArgumentNullException(nameof(accessToken), "you are not authenticated");

            using var channel = GrpcChannel.ForAddress(Settings.ServerAddress);

            var client = new TaskItem.TaskItemClient(channel);

            var request = new TaskListRequest
            {
                AccessToken = accessToken
            };

            var response = await client.ListAllAsync(request);

            var tasks = response.Items.OrderByDescending(x => x.CreatedAt);

            ConsoleWritter.WriteInfo("==== ALL TASKS ===\n");
            ConsoleWritter.WriteWithColor("+ completed", ConsoleColor.Green);
            ConsoleWritter.WriteWithColor("- uncompleted", ConsoleColor.White);
            Console.WriteLine();

            StringBuilder sb = new();

            foreach (var task in tasks)
            {
                var id = task.Id[0..4];

                sb.Append(task.Completed ? "+" : "- ");

                if (_parameters.Has("--full"))
                {
                    var createdAt = new DateTime(task.CreatedAt);

                    sb.Append($"[{id}]    ");
                    sb.Append($"[{createdAt:MM/dd HH:mm}]    ");
                }

                sb.Append(task.Name);

                if (task.Completed)
                    ConsoleWritter.WriteWithColor(sb.ToString(), ConsoleColor.Green);
                else
                    ConsoleWritter.WriteWithColor(sb.ToString(), ConsoleColor.White);

                sb.Clear();
            }

            Console.WriteLine();
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
