using GrpcTodo.CLI.UseCases.Common;

namespace GrpcTodo.CLI.UseCases.TaskCreate;

public sealed class TaskCreatePrompt : Prompt
{
    public TaskCreatePromptOutput PromptName()
    {
        var name = Read("task name: ", new PromptOptions
        {
            RemoveWhitespaces = true
        });

        return new TaskCreatePromptOutput(name);
    }
}

public sealed record TaskCreatePromptOutput(string Name);
