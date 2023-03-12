using GrpcTodo.Server.Domain.Repositories;
using GrpcTodo.Server.Domain.Services;

namespace GrpcTodo.Server.Domain.UseCases.User;

public sealed class CreateUserUseCase
{
    private readonly IPasswordHashingService _passwordHashingService;
    private readonly IUserRepository _userRepository;
    private readonly IAuthTokenGenerator _authTokenGenerator;

    public CreateUserUseCase(
        IUserRepository userRepository,
        IPasswordHashingService passwordHashingService,
        IAuthTokenGenerator authTokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHashingService = passwordHashingService;
        _authTokenGenerator = authTokenGenerator;
    }

    private void Validate(CreateUserUseCaseInput input)
    {
        if (string.IsNullOrWhiteSpace(input.Username))
            throw new InvalidUserNameException("invalid user name");

        if (string.IsNullOrEmpty(input.Password) || input.Password.Length < 8)
            throw new InvalidUserPasswordException("password too short. minimum of 8 digits");
    }

    public async Task<string> ExecuteAsync(CreateUserUseCaseInput input)
    {
        Validate(input);

        var userOnDatabase = await _userRepository.FindByNameAsync(input.Username);

        if (userOnDatabase is not null)
            throw new ConflictException("an user with this same name already exists");

        var passwordHash = _passwordHashingService.Hash(input.Password);

        var token = _authTokenGenerator.Generate();

        await _userRepository.CreateAsync(input.Username, passwordHash, token);

        return token.ToString();
    }
}

public sealed record CreateUserUseCaseInput(string Username, string Password);