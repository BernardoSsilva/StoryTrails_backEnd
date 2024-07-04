using StoryTrails.Communication.Responses.Books;


namespace StoryTrails.Application.UseCases.Books.interfaces
{
    public interface IFindBookByIdUseCase
    {
        Task<BookDetailedResponse> Execute(string id, string userToken);
    }
}
