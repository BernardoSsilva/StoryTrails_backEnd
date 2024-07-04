using StoryTrails.Communication.Request;

namespace StoryTrails.Application.UseCases.Users.interfaces
{
    public interface IUpdateUserUseCase
    {
        Task Execute(string id, UserJsonRequest requestBody, string userToken);
    }
}
