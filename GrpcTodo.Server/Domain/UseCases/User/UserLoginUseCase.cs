using GrpcTodo.Server.Domain.Repositories;
using GrpcTodo.Server.Domain.Services;

namespace GrpcTodo.Server.Domain.UseCases.User;

public sealed class UserLoginUseCase
{
    private readonly IPasswordHashingService _passwordHashingService;
    private readonly IUserRepository _userRepository;

    public UserLoginUseCase(IUserRepository userRepository, IPasswordHashingService passwordHashingService)
    {
        _userRepository = userRepository;
        _passwordHashingService = passwordHashingService;
    }

    private void Validate(UserLoginUseCaseInput input)
    {
        if (string.IsNullOrWhiteSpace(input.Username))
            throw new InvalidUserNameException("invalid user name");

        if (string.IsNullOrEmpty(input.Password) || input.Password.Length < 8)
            throw new InvalidUserPasswordException("password too short. minimum of 8 digits");
    }

    public async Task<Entities.User> ExecuteAsync(UserLoginUseCaseInput input)
    {
        Validate(input);

        var userOnDatabase = await _userRepository.FindByNameAsync(input.Username);

        if (userOnDatabase is null)
            throw new UnauthorizedException("user and/or password are invalid");

        var passwordHash = _passwordHashingService.Hash(input.Password);

        if (!userOnDatabase.Password.Equals(passwordHash))
            throw new UnauthorizedException("user and/or password are invalid");

        return userOnDatabase;
    }
}

public sealed record UserLoginUseCaseInput(string Username, string Password);