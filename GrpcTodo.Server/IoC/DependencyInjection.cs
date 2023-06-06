using GrpcTodo.Server.Application.Repositories;
using GrpcTodo.Server.Application.Middleware;
using GrpcTodo.Server.Application.Services;
using GrpcTodo.Server.Domain.Repositories;
using GrpcTodo.Server.Domain.Services;
using GrpcTodo.Server.Domain.Middleware;
using GrpcTodo.Server.Domain.UseCases.User;
using GrpcTodo.Server.Domain.UseCases.Tasks;
using GrpcTodo.Server.Infra.Context;
using GrpcTodo.Server.Infra.Interfaces;

namespace GrpcTodo.Server.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddIoC(this IServiceCollection services)
    {
        services.AddSingleton<IContext, DapperContext>();

        // repositories
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ITaskRepository, TaskRepository>();

        // services
        services.AddSingleton<IPasswordHashingService, Sha256PasswordHash>();
        services.AddSingleton<IAuthTokenGenerator, AuthTokenGenerator>();
        services.AddSingleton<IGuidGenerator, GuidGenerator>();

        // use cases
        services.AddScoped<DeleteTaskUseCase>();
        services.AddScoped<CreateUserUseCase>();
        services.AddScoped<UserLoginUseCase>();
        services.AddScoped<UpdateTokenUseCase>();
        services.AddScoped<CreateTaskUseCase>();
        services.AddScoped<ListAllTasksUseCase>();

        // middleware
        services.AddTransient<IAuthMiddleware, AuthMiddleware>();

        return services;
    }
}
