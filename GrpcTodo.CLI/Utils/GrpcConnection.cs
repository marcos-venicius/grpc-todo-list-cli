using Grpc.Net.Client;

namespace GrpcTodo.CLI.Utils;

public static class GrpcConnection
{
    public static GrpcChannel CreateChannel()
    {
        var httpHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

        var configurations = new GrpcChannelOptions
        {
            HttpHandler = httpHandler
        };

        return GrpcChannel.ForAddress(Settings.ServerAddress, configurations);
    }
}