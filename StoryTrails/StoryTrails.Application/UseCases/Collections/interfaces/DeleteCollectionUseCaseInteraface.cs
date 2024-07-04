namespace StoryTrails.Application.UseCases.Collections.interfaces
{
    public interface IDeleteCollectionUseCase
    {
        Task<bool> Execute(string id, string userToken);

    }
}
