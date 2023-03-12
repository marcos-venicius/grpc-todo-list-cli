using GrpcTodo.Server.Domain.Repositories;
using GrpcTodo.Server.Domain.Services;

namespace GrpcTodo.Server.Domain.UseCases.User;

public sealed class UpdateTokenUseCase
{
    private readonly IAuthTokenGenerator _authTokenGenerator;
    private readonly IUserRepository _userRepository;

    public UpdateTokenUseCase(
        IAuthTokenGenerator authTokenGenerator,
        IUserRepository userRepository)
    {
        _authTokenGenerator = authTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<Guid> ExecuteAsync(Guid userId)
    {
        var newToken = _authTokenGenerator.Generate();

        await _userRepository.UpdateTokenAsync(userId, newToken);

        return newToken;
    }
}