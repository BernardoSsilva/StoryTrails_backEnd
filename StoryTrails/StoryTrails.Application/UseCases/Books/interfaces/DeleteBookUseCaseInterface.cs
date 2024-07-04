namespace StoryTrails.Application.UseCases.Books.interfaces
{
    public interface IDeleteBookUseCase
    {
        Task Execute(string id, string userToken);
    }
}
