using Grpc.Core;
using GrpcTodo.Server.Domain.UseCases.User;
using GrpcTodo.SharedKernel.Protos.User;
using GrpcTodo.SharedKernel.Protos.User.Requests;
using GrpcTodo.SharedKernel.Protos.User.Responses;

namespace GrpcTodo.Server.GrpcServices;

public sealed class UserGrpcService : User.UserBase
{
    private readonly CreateUserUseCase _createUserUseCase;
    private readonly UserLoginUseCase _userLoginUseCase;
    private readonly UpdateTokenUseCase _updateTokenUseCase;

    public UserGrpcService(
        CreateUserUseCase createUserUseCase,
        UserLoginUseCase userLoginUseCase,
        UpdateTokenUseCase updateTokenUseCase)
    {
        _createUserUseCase = createUserUseCase;
        _userLoginUseCase = userLoginUseCase;
        _updateTokenUseCase = updateTokenUseCase;
    }

    public override async Task<UserCreateResponse> Create(UserCreateRequest request, ServerCallContext context)
    {
        var input = new CreateUserUseCaseInput(request.Name, request.Password);

        var token = await _createUserUseCase.ExecuteAsync(input);

        return new UserCreateResponse { Token = token };
    }

    public override async Task<UserLoginResponse> Login(UserLoginRequest request, ServerCallContext context)
    {
        var input = new UserLoginUseCaseInput(request.Name, request.Password);

        var user = await _userLoginUseCase.ExecuteAsync(input);

        return new UserLoginResponse { Token = user.Token.ToString() };
    }

    public override async Task<UserUpdateTokenResponse> UpdateToken(UserUpdateTokenRequest request, ServerCallContext context)
    {
        var input = new UserLoginUseCaseInput(request.Name, request.Password);

        var user = await _userLoginUseCase.ExecuteAsync(input);

        var newAuthToken = await _updateTokenUseCase.ExecuteAsync(user.Id);

        return new UserUpdateTokenResponse { Token = newAuthToken.ToString() };
    }
}
