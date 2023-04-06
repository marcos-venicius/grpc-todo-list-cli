namespace GrpcTodo.Server.Application.Common;

public delegate Response Runner<Request, Response>(Guid userId, Request request)
    where Request : class
    where Response : notnull;

public delegate Response Runner<Response>(Guid userId)
    where Response : notnull;
