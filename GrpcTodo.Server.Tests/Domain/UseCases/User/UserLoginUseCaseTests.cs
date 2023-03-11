using GrpcTodo.Server.Domain.Repositories;
using GrpcTodo.Server.Domain.Services;
using GrpcTodo.Server.Domain.UseCases.User;

namespace GrpcTodo.Server.Tests.Domain.UseCases.User;


public sealed partial class UserLoginUseCaseTests
{
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public async Task Should_Throw_InvalidUserNameException_When_Username_Is_Invalid(string username)
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
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public async Task Should_Throw_InvalidUserPasswordException_When_Username_Is_Invalid(string password)
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
    public async Task Shoudl_Throw_UnauthorizedException_When_User_Does_Not_Exists()
    {
        var input = InputFactory.Create();
        var sut = Sut();

        var exception = await Assert.ThrowsAsync<UnauthorizedException>(async () =>
        {
            await sut.ExecuteAsync(input);
        });

        Assert.Equal("user and/or password are invalid", exception.Message);
    }

    [Fact]
    public async Task Shoudl_Throw_UnauthorizedException_When_User_Password_Is_Wrong()
    {
        var input = InputFactory.Create();

        _userRepositoryMock
            .Setup(x => x.FindByNameAsync(input.Username))
            .ReturnsAsync(new Server.Domain.Entities.User
            {
                Password = "sdlfkjdsflkj"
            });

        var sut = Sut();

        var exception = await Assert.ThrowsAsync<UnauthorizedException>(async () =>
        {
            await sut.ExecuteAsync(input);
        });

        Assert.Equal("user and/or password are invalid", exception.Message);
    }

    [Fact]
    public async Task Should_Make_Login_Successfully_When_Everything_Is_Fine()
    {
        var guid = Guid.NewGuid();
        var input = InputFactory.Create();

        _userRepositoryMock
            .Setup(x => x.FindByNameAsync(input.Username))
            .ReturnsAsync(new Server.Domain.Entities.User
            {
                Password = "test",
                Token = guid
            });

        _passwordHashingServiceMock
            .Setup(x => x.Hash(input.Password))
            .Returns("test");

        var sut = Sut();

        var token = await sut.ExecuteAsync(input);

        Assert.Equal(guid.ToString(), token);
    }
}

#region Setup

file static class InputFactory
{
    public static UserLoginUseCaseInput Create()
    {
        return new UserLoginUseCaseInput("user", "password123");
    }
}

public sealed partial class UserLoginUseCaseTests
{
    private readonly Mock<IPasswordHashingService> _passwordHashingServiceMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;

    public UserLoginUseCaseTests()
    {
        _passwordHashingServiceMock = new Mock<IPasswordHashingService>();
        _userRepositoryMock = new Mock<IUserRepository>();
    }

    private UserLoginUseCase Sut()
    {
        return new UserLoginUseCase(
            _userRepositoryMock.Object,
            _passwordHashingServiceMock.Object
        );
    }
}

#endregion