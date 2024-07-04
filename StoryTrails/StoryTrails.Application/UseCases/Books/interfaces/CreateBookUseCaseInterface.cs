using StoryTrails.Communication.Request;
using StoryTrails.Communication.Responses.Books;

namespace StoryTrails.Application.UseCases.Books.interfaces
{
    public interface ICreateBookUseCase
    {
        Task<BookShortResponse> Execute(BooksJsonRequest request, string userToken);
    }
}
