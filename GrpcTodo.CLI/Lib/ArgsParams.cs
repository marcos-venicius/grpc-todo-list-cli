using GrpcTodo.CLI.Models;

namespace GrpcTodo.CLI.Lib;

public sealed class ArgsParams
{
    private readonly Parameters _params;
    private readonly string[] _args;

    public ArgsParams(string[] args)
    {
        _args = args;
        _params = new Parameters();
    }

    public void Set(string param, ParamDetail detail)
    {
        if (_params.Has(param))
            throw new Exception("this param already exists");

        _params[param] = detail;
    }

    public Parameters Read()
    {
        Parameters paramsFound = new();

        foreach (var arg in _args)
            if (_params.Has(arg))
                paramsFound[arg] = _params[arg];

        return paramsFound;
    }
}