using Microsoft.Extensions.DependencyInjection;
using StoryTrails.API.mappers;
using StoryTrails.API.UseCases.Books;
using StoryTrails.Application.UseCases.Books;
using StoryTrails.Application.UseCases.Books.interfaces;
using StoryTrails.Application.UseCases.Collections;
using StoryTrails.Application.UseCases.Collections.interfaces;
using StoryTrails.Application.UseCases.Users.interfaces;
using StoryTrails.Application.UseCases.Users;


namespace StoryTrails.Application
{
    public static class DependencyInjectionExtension
    {

        public static void AddApplication(this IServiceCollection service)
        {

            AddAutoMapper(service);
            AddUseCases(service);
        }


        private static void AddAutoMapper(IServiceCollection service)
        {
            service.AddAutoMapper(typeof(AutoMapping));
        }
        private static void AddUseCases(IServiceCollection service)
        {
            service.AddScoped<IFindAllBooksUseCase, FindAllBooksUsecase>();
            service.AddScoped<ICreateBookUseCase, CreateBookUseCase>();
            service.AddScoped<IFindBookByIdUseCase, FindBookByIdUseCase>();
            service.AddScoped<IFindBooksByCollectionUseCase, FindBooksByCollectionUseCase>();
            service.AddScoped<IUpdateBookInfoUseCase, UpdateBookUseCase>();
            service.AddScoped<IDeleteBookUseCase, DeleteBookUseCase>();
            service.AddScoped<ICreateCollectionUseCase, CreateCollectionUseCase>();
            service.AddScoped<IGetAllCollectionsUseCase, GetAllCollectionsUseCase>();
            service.AddScoped<IFindCollectionByIdUseCase, FindCollectionByIdUseCase>();
            service.AddScoped<IUpdateCollectionUseCase, UpdateCollectionUseCase>();
            service.AddScoped<IDeleteCollectionUseCase, DeleteCollectionUseCase>();
            service.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            service.AddScoped<IFindAllUsersUseCase, FindAllUsersUseCase>();
            service.AddScoped<IFindUserByIdUseCase, FindUserByIdUseCase>();
            service.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
            service.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();
             service.AddScoped< IAuthenticateUserUseCase, AuthenticateUserUseCase  > ();

        }
    }

}
