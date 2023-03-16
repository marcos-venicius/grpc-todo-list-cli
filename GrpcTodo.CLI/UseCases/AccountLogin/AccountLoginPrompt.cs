using GrpcTodo.CLI.UseCases.Common;

namespace GrpcTodo.CLI.UseCases.AccountLogin;

public sealed class AccountLoginPrompt : Prompt
{
    public AccountLoginPromptOutput Prompt()
    {
        var username = Read("username: ", new PromptOptions
        {
            RemoveWhitespaces = true
        });

        var password = Read("password: ");

        return new AccountLoginPromptOutput(username, password);
    }
}

public sealed record AccountLoginPromptOutput(string Username, string Password);