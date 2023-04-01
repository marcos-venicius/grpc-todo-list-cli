using System.Reflection;
using FluentMigrator.Runner;
using GrpcTodo.Server.GrpcServices;
using GrpcTodo.Server.Infra;
using GrpcTodo.Server.IoC;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(c => c.AddFluentMigratorConsole())
        .AddFluentMigratorCore()
        .ConfigureRunner(c => c.AddPostgres()
            .WithGlobalConnectionString(builder.Configuration.GetConnectionString("postgresql"))
            .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations());

builder.Services.AddIoC();

// builder.Services.AddControllers();

builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .WriteTo.Console()
        .WriteTo.File("Logs/server-.log", rollingInterval: RollingInterval.Day);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<UserGrpcService>();
app.MapGrpcService<TaskGrpcService>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseAuthorization();

// app.MapControllers();

app.RunMigrations(app.Environment);

app.Run();
