using StoryTrails.Comunication.Responses.Books;

namespace StoryTrails.Application.UseCases.Books.interfaces
{
    public interface IFindAllBooksUseCase
    {
        Task<MultipleBooksResponse> Execute();
    }
}
