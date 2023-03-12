using GrpcTodo.Server.Domain.Repositories;
using GrpcTodo.Server.Domain.Services;
using GrpcTodo.Server.Domain.UseCases.User;

namespace GrpcTodo.Server.Tests.Domain.UseCases.User;

public sealed partial class CreateUserUseCaseTests
{
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public async Task Should_Throw_InvalidUserNameException_When_User_Name_Is_Invalid(string username)
    {
        var input = InputFactory.Create() with { Username = username };

        var sut = Sut();

        var exception = await Assert.ThrowsAsync<InvalidUserNameException>(async () =>
        {
            await sut.ExecuteAsync(input);
        });

        Assert.Equal("invalid user name", exception.Message);
    }

    [Theory]
    [InlineData("1234567")]
    [InlineData("       ")]
    public async Task Should_Throw_InvalidUserPasswordException_When_Password_Is_Short(string password)
    {
        var input = InputFactory.Create() with { Password = password };

        var sut = Sut();

        var exception = await Assert.ThrowsAsync<InvalidUserPasswordException>(async () =>
        {
            await sut.ExecuteAsync(input);
        });

        Assert.Equal("password too short. minimum of 8 digits", exception.Message);
    }

    [Fact]
    public async Task Should_Throw_ConflictException_When_Already_Exists_User_With_Same_Name()
    {
        _userRepositoryMock
            .Setup(x => x.FindByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(new Server.Domain.Entities.User());

        var input = InputFactory.Create();

        var sut = Sut();

        var exception = await Assert.ThrowsAsync<ConflictException>(async () =>
        {
            await sut.ExecuteAsync(input);
        });

        Assert.Equal("an user with this same name already exists", exception.Message);
    }

    [Fact]
    public async Task Should_Successfully_Create_A_New_User()
    {
        var authToken = Guid.NewGuid();

        _authTokenGeneratorMock
            .Setup(x => x.Generate())
            .Returns(authToken);

        _passwordHashingServiceMock
            .Setup(x => x.Hash(It.IsAny<string>()))
            .Returns("PASSWORD_HASH");

        var input = InputFactory.Create();

        var sut = Sut();

        await sut.ExecuteAsync(input);

        _userRepositoryMock.Verify(x => x.CreateAsync(input.Username, "PASSWORD_HASH", authToken), Times.Once);
    }
}

#region Setup

file static class InputFactory
{
    public static CreateUserUseCaseInput Create()
    {
        return new CreateUserUseCaseInput("user", "password123");
    }
}

public sealed partial class CreateUserUseCaseTests
{
    private readonly Mock<IPasswordHashingService> _passwordHashingServiceMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IAuthTokenGenerator> _authTokenGeneratorMock;

    public CreateUserUseCaseTests()
    {
        _passwordHashingServiceMock = new Mock<IPasswordHashingService>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _authTokenGeneratorMock = new Mock<IAuthTokenGenerator>();
    }

    private CreateUserUseCase Sut()
    {
        return new CreateUserUseCase(
            _userRepositoryMock.Object,
            _passwordHashingServiceMock.Object,
            _authTokenGeneratorMock.Object
        );
    }
}

#endregion