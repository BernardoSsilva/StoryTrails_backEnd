using StoryTrails.Comunication.Responses.Collections;

namespace StoryTrails.Application.UseCases.Collections.interfaces
{
    public interface IFindCollectionByIdUseCase
    {
        Task<CollectionSingleResponse>  Execute(string id);
    }
}
