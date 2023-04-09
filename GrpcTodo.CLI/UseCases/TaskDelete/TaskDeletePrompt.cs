using System.Text;

using GrpcTodo.CLI.UseCases.Common;
using GrpcTodo.CLI.Utils;
using GrpcTodo.SharedKernel.Protos.Tasks.Responses;

namespace GrpcTodo.CLI.UseCases.TaskDelete;

public sealed class TaskDeletePrompt : Prompt
{
    public string PromptTask(IOrderedEnumerable<TaskListResponseItem> tasks)
    {
        Dictionary<string, bool> tasksId = new();

        foreach (var task in tasks)
            tasksId.Add(task.Id[0..4], true);

        ConsoleWritter.WriteInfo("Choose a task id to delete\n");

        ConsoleWritter.WriteWithColor("+ completed", ConsoleColor.Green);
        ConsoleWritter.WriteWithColor("- uncompleted", ConsoleColor.White);
        Console.WriteLine();

        StringBuilder sb = new();

        foreach (var task in tasks)
        {
            var id = task.Id[0..4];

            sb.Append(task.Completed ? "+" : "- ");

            var createdAt = new DateTime(task.CreatedAt);

            sb.Append($"[{id}]    [{createdAt:MM/dd HH:mm}]    {task.Name}");

            if (task.Completed)
                ConsoleWritter.WriteWithColor(sb.ToString(), ConsoleColor.Green);
            else
                ConsoleWritter.WriteWithColor(sb.ToString(), ConsoleColor.White);

            sb.Clear();
        }

        Console.WriteLine();

        var taskId = Read("task id: ", new PromptOptions
        {
            RemoveWhitespaces = true,
            ShouldBeSingleWord = true,
            CustomMessage = "This task id does not exists",
            Custom = id => !tasksId.ContainsKey(id)
        });

        return taskId;
    }
}
