using Client.Utils;
using Grpc.Net.Client;

namespace Client.UseCases;

internal sealed class CreateTodoUseCase
{
    public void Execute()
    {
        ConsoleWritter.WriteLine("+ create new todo");

        var name = ConsoleReader.ReadString("name: ");
        var description = ConsoleReader.ReadString("description?: ");

        if (string.IsNullOrWhiteSpace(name))
            throw new ShowErrorMessageException("the name cannot be null");

        using var channel = GrpcChannel.ForAddress("https://localhost:5001");

        var client = new Todo.TodoClient(channel);

        var request = new CreateTodoRequest
        {
            Description = description,
            Name = name
        };

        var response = client.Create(request);

        Console.WriteLine($"* todo created with id {response.Id}");

        Console.ReadKey();
    }
}