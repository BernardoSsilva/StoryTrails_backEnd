using StoryTrails.Communication.Request;
using StoryTrails.Communication.Responses.Users;

namespace StoryTrails.Application.UseCases.Users.interfaces
{
    public interface IAuthenticateUserUseCase
    {
        Task<UserAuthenticationResponse> Execute(UserAuthenticationBody requestBody);
    }
}