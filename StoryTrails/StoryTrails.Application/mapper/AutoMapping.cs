using AutoMapper;
using StoryTrails.Communication.Request;
using StoryTrails.Communication.Responses.Books;
using StoryTrails.Communication.Responses.Collections;
using StoryTrails.Communication.Responses.Users;
using StoryTrails.Domain.Entities;


namespace StoryTrails.API.mappers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToEntity();
            EntityToResponse();
        }

        private void RequestToEntity()
        {
            CreateMap<BooksJsonRequest, Book>();
            CreateMap<CollectionJsonRequest, Collection>();
            CreateMap<UserJsonRequest, User>();


        }

        private void EntityToResponse()
        {
            CreateMap<Book, BookShortResponse>();
            CreateMap<Book, BookDetailedResponse>();
            CreateMap<BooksJsonRequest, BookShortResponse>();
            CreateMap<CollectionJsonRequest, CollectionSingleResponse>();
            CreateMap<Collection, CollectionSingleResponse>();
            CreateMap<User, UserResponse>();



        }
    }
}
