using Grpc.Core;
using Texnokaktus.ProgOlymp.Common.Contracts.Grpc.YandexId;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.Services.Abstractions;
using YandexOAuthClient.Abstractions;

namespace Texnokaktus.ProgOlymp.YandexIdIntegrationService.Services.Grpc;

public class UserServiceImpl(IAuthService authService, IYandexIdClient yandexIdClient) : UserService.UserServiceBase
{
    public override Task<GetOAuthUrlResponse> GetOAuthUrl(GetOAuthUrlRequest request, ServerCallContext context) =>
        Task.FromResult(new GetOAuthUrlResponse
        {
            Result = authService.GetOAuthUrl(request.RedirectUrl)
        });

    public override async Task<AuthenticateUserResponse> AuthenticateUser(AuthenticateUserRequest request, ServerCallContext context)
    {
        var token = await authService.GetAccessTokenAsync(request.Code);
        var userData = await yandexIdClient.GetUserDataAsync(token);

        return new()
        {
            Result = new()
            {
                Login = userData.Login,
                DisplayName = userData.DisplayName,
                Avatar = userData.IsAvatarEmpty.HasValue
                             ? new Avatar
                             {
                                 AvatarId = userData.DefaultAvatarId
                             }
                             : null
            }
        };
    }
}
