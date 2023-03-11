using FluentMigrator.Runner;

namespace GrpcTodo.Server.Infra;

public static class MigrationManager
{
    public static IHost RunMigrations(this IHost host, IWebHostEnvironment environment)
    {
        using var scope = host.Services.CreateScope();

        var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

        if (environment.IsDevelopment())
            migrationService.ListMigrations();

        migrationService.MigrateUp();

        return host;
    }
}