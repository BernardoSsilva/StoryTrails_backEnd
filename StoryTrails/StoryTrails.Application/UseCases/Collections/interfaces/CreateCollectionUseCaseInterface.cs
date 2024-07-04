using StoryTrails.Comunication.Request;

namespace StoryTrails.Application.UseCases.Collections.interfaces
{
    public interface ICreateCollectionUseCase
    {
        Task Execute(CollectionJsonRequest request, string userToken);
    }
}
