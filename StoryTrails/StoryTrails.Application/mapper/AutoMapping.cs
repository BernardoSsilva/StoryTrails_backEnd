using AutoMapper;
using StoryTrails.Comunication.Request;
using StoryTrails.Comunication.Responses.Books;
using StoryTrails.Domain.Entities;


namespace StoryTrails.API.mappers
{
    public class AutoMapping:Profile
    {
        public AutoMapping()
        {
            RequestToEntity();
            EntityToResponse();
        }

        private void RequestToEntity()
        {
             CreateMap<BooksJsonRequest, Book>();
        }

        private void EntityToResponse()
        {
            CreateMap<Book, BookShortResponse>();
            CreateMap<Book, BookDetailedResponse>();
            CreateMap<BooksJsonRequest, BookShortResponse>();
        }
    }
}
