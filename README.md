# grpc-todo-list-live

A simple grpc todo list on live. your todo list as CLI

## How to run on your machine?

- you need to install [.NET 7](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)

to run and test the application, first build the solution.

```bash
dotnet build
```

after build the solution the proto files will be "transformed" into csharp classes and will be accessible to you on csharp code.

to run the server project

```bash
cd ./Server
```

```bash
dotnet run -lp https
```

this will run the application on https profile

to the the application with the client


```bash
cd ./Client
```

you have two options to run the client.

- As CLI, using the [available commands list](#available-commands-list)

```bash
dotnet run <command>
```

- As a normal program

```bash
dotnet run
```

### Available commands list

- `lt` list todos. this will list all your todos
- `ct` create todo. this command allow you to create a todo.
