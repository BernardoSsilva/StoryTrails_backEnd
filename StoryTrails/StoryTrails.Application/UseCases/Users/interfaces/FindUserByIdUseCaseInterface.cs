using StoryTrails.Communication.Responses.Users;

namespace StoryTrails.Application.UseCases.Users.interfaces
{
    public interface IFindUserByIdUseCase
    {

        Task<UserResponse> Execute(string id);
    }
}
