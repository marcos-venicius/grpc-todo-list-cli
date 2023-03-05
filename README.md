# Todo list as CLI

<div align="center">
  <img src="./docs/images/structure.png">
</div>

A simple grpc todo list on live. your todo list as CLI

## How to run on your machine?

- you need to install [.NET 7](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- you need to have [Docker](https://www.docker.com/) installed (latest version)

build project to generate the proto files as c# classes

```bash
dotnet build
```

up all docker containers

```bash
docker compose up -d
```

run the server to accept the connections

```bash
cd GrpcTodo.Server
```

```bash
dotnet run -lp https
```

## Proto VSCode Extension help

settings.json

```json
{
  "protoc": {
    "options": ["--proto_path=GrpcTodo.SharedKernel/Protos"]
  }
}
```
