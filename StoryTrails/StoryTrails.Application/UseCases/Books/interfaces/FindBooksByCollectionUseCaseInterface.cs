using StoryTrails.Communication.Responses.Books;

namespace StoryTrails.Application.UseCases.Books.interfaces
{
    public interface IFindBooksByCollectionUseCase
    {
        Task<MultipleBooksResponse> Execute(string collectionId, string userToken);

    }
}
