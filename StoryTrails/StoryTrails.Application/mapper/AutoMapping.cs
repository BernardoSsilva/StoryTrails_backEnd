using AutoMapper;
using StoryTrails.Comunication.Request;
using StoryTrails.Comunication.Responses.Books;
using StoryTrails.Comunication.Responses.Collections;
using StoryTrails.Comunication.Responses.Users;
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
