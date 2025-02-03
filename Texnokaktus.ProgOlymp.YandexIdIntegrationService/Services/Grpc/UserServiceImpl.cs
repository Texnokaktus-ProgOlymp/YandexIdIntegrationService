using Grpc.Core;
using Texnokaktus.ProgOlymp.Common.Contracts.Grpc.YandexId;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.Logic.Services.Abstractions;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.YandexClient.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.YandexIdIntegrationService.Services.Grpc;

public class UserServiceImpl(IUserDataService userDataService, IYandexAuthenticationService yandexAuthenticationService) : UserService.UserServiceBase
{
    public override Task<GetOAuthUrlResponse> GetOAuthUrl(GetOAuthUrlRequest request, ServerCallContext context) =>
        Task.FromResult(new GetOAuthUrlResponse
        {
            Result = yandexAuthenticationService.GetYandexOAuthUrl(request.RedirectUrl)
        });

    public override async Task<AuthenticateUserResponse> AuthenticateUser(AuthenticateUserRequest request, ServerCallContext context)
    {
        var user = await userDataService.AuthenticateUserAsync(request.Code);

        return new()
        {
            Result = user.MapUser()
        };
    }

    public override async Task<GetUserResponse> GetUser(GetUserRequest request, ServerCallContext context)
    {
        if (await userDataService.GetUserInfoAsync(request.Login) is not { } user)
            throw new RpcException(new(StatusCode.NotFound, $"User with login '{request.Login}' cannot be found"));

        return new()
        {
            Result = user.MapUser()
        };
    }
}

file static class MappingExtensions
{
    public static User MapUser(this Domain.User user) =>
        new()
        {
            Login = user.Login,
            DisplayName = user.DisplayName,
            Avatar = user.Avatar?.MapAvatar()
        };

    private static Avatar MapAvatar(this Domain.Avatar avatar) =>
        new()
        {
            AvatarId = avatar.AvatarId
        };
}
