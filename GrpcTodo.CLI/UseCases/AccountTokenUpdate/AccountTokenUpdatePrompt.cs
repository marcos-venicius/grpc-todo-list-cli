using GrpcTodo.CLI.UseCases.Common;

namespace GrpcTodo.CLI.UseCases.AccountTokenUpdate;

public sealed class AccountTokenUpdatePrompt : Prompt
{
    public AccountUpdateTokenPromptOutput Prompt()
    {
        var username = Read("username: ", new PromptOptions
        {
            RemoveWhitespaces = true
        });

        var password = Read("password: ", new PromptOptions
        {
            ShouldBeHidden = true,
            HiddenSymbol = "*"
        });

        return new AccountUpdateTokenPromptOutput(username, password);
    }
}

public sealed record AccountUpdateTokenPromptOutput(string Username, string Password);
