using GrpcTodo.CLI.Models;

namespace GrpcTodo.CLI.Lib;

public sealed class Parameters
{
    private readonly Dictionary<string, ParamDetail> _parameters;

    public Parameters()
    {
        _parameters = new Dictionary<string, ParamDetail>();
    }

    public ParamDetail this[string parameter]
    {
        get => _parameters[parameter];
        set => _parameters.Add(parameter, value);
    }

    public bool Has(string parameter)
    {
        return _parameters.ContainsKey(parameter);
    }
}