using GrpcTodo.Server.Domain.Repositories;
using GrpcTodo.Server.Domain.Services;
using GrpcTodo.Server.Domain.UseCases.User;

namespace GrpcTodo.Server.Tests.Domain.UseCases.User;

public sealed partial class UpdateTokenUseCaseTests
{
    [Fact]
    public async Task Should_Create_A_New_Auth_Token_Successfully()
    {
        var userId = Guid.NewGuid();
        var authToken = Guid.NewGuid();

        _authTokenGeneratorMock
            .Setup(x => x.Generate())
            .Returns(authToken);

        var sut = Sut();

        await sut.ExecuteAsync(userId);

        _userRepositoryMock.Verify(x => x.UpdateTokenAsync(userId, authToken), Times.Once);
    }
}

#region Setup

public sealed partial class UpdateTokenUseCaseTests
{
    private readonly Mock<IAuthTokenGenerator> _authTokenGeneratorMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;

    public UpdateTokenUseCaseTests()
    {
        _authTokenGeneratorMock = new Mock<IAuthTokenGenerator>();
        _userRepositoryMock = new Mock<IUserRepository>();
    }

    private UpdateTokenUseCase Sut()
    {
        return new UpdateTokenUseCase(
            _authTokenGeneratorMock.Object,
            _userRepositoryMock.Object
        );
    }
}

#endregion
