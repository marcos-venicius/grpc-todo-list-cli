using GrpcTodo.CLI.Utils;

namespace GrpcTodo.CLI.UseCases;

public sealed class AccountCreatePrompt
{
    private string _username = "";
    private string _password = "";

    private bool _usernameIsGet = false;
    private bool _passwordIsGet = false;

    public AccountCreatePromptOutput Prompt()
    {
        if (!_usernameIsGet)
        {
            ConsoleWritter.Write("username: ");

            _username = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(_username))
            {
                ConsoleWritter.WriteError("username cannot be empty");
                return Prompt();
            }

            _usernameIsGet = true;
        }

        if (!_passwordIsGet)
        {
            ConsoleWritter.Write("password: ");

            _password = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(_password))
            {
                ConsoleWritter.WriteError("password cannot be empty");
                return Prompt();
            }

            _passwordIsGet = true;
        }

        return new AccountCreatePromptOutput(_username, _password);
    }
}

public sealed record AccountCreatePromptOutput(string Username, string Password);