using StoryTrails.Communication.Responses.Collections;

namespace StoryTrails.Application.UseCases.Collections.interfaces
{
    public interface IGetAllCollectionsUseCase
    {
        Task<MultipleCollectionResponse> Execute(string userToken);
    }
}
