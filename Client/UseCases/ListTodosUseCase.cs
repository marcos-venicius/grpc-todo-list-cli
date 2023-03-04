using Client.Utils;
using Grpc.Net.Client;

namespace Client.UseCases;

public sealed class ListTodosUseCase
{
    public void Execute()
    {
        ConsoleWritter.WriteLine("MY TODO LIST", true);

        using var channel = GrpcChannel.ForAddress("https://localhost:5001");

        var client = new Todo.TodoClient(channel);

        var response = client.List(new Google.Protobuf.WellKnownTypes.Empty());

        foreach (var todoItem in response.Items)
        {
            Console.WriteLine($"[{todoItem.Id}] {todoItem.Name}");

            if (!string.IsNullOrWhiteSpace(todoItem.Description))
                Console.WriteLine($"\t{todoItem.Description}");

            Console.WriteLine();
        }

        Console.ReadKey();
    }
}