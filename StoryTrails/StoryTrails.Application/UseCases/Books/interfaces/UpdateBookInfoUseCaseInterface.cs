using StoryTrails.Comunication.Request;

namespace StoryTrails.Application.UseCases.Books.interfaces
{
    public interface IUpdateBookInfoUseCase
    {
        Task Execute(string id, BooksJsonRequest requestBody);
    }
}
