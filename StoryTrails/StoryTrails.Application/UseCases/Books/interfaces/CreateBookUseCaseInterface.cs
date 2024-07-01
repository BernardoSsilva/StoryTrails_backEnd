using StoryTrails.Comunication.Request;
using StoryTrails.Comunication.Responses.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryTrails.Application.UseCases.Books.interfaces
{
    public interface ICreateBookUseCase
    {
        Task<BookShortResponse> Execute(BooksJsonRequest request);
    }
}
