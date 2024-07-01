using Microsoft.Extensions.DependencyInjection;
using StoryTrails.API.mappers;
using StoryTrails.API.UseCases.Books;
using StoryTrails.Application.UseCases.Books;
using StoryTrails.Application.UseCases.Books.interfaces;

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
        
        }
    }

}
