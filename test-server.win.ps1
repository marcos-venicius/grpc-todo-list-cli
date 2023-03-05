clear;

$sut = $args[0];

dotnet build;

if ($sut)
{
    dotnet test --no-build --no-restore --nologo --logger="console;verbosity=detailed" --filter FullyQualifiedName~$sut ./GrpcTodo.Server.Tests/GrpcTodo.Server.Tests.csproj;
}
else
{
    dotnet test --no-build --no-restore --nologo --logger="console;verbosity=detailed" ./GrpcTodo.Server.Tests/GrpcTodo.Server.Tests.csproj;
}