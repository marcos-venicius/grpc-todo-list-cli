using GrpcTodo.Server.Infra.Context;
using GrpcTodo.Server.Infra.Interfaces;

namespace GrpcTodo.Server.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddIoC(this IServiceCollection services)
    {
        services.AddSingleton<IContext, DapperContext>();

        return services;
    }
}