using GrpcTodo.Server.Infra.Context;

namespace GrpcTodo.Server.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddIoC(this IServiceCollection services)
    {
        services.AddSingleton<DapperContext>();

        return services;
    }
}