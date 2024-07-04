

namespace StoryTrails.Application.UseCases.Users.interfaces
{
    public interface IDeleteUserUseCase
    {
        Task Execute(string id);
    }
}