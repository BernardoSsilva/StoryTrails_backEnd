using StoryTrails.Comunication.Request;
using StoryTrails.Comunication.Responses.Users;

namespace StoryTrails.Application.UseCases.Users.interfaces
{
    public interface IAuthenticateUserUseCase
    {
        Task<UserAuthenticationResponse> Execute(UserAuthenticationBody requestBody);
    }
}