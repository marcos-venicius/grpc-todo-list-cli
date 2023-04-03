using GrpcTodo.CLI.UseCases.Common;

namespace GrpcTodo.CLI.UseCases.AccountCreate;

public sealed class AccountCreatePrompt : Prompt
{
    public AccountCreatePromptOutput Prompt()
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

        return new AccountCreatePromptOutput(username, password);
    }
}

public sealed record AccountCreatePromptOutput(string Username, string Password);