using StoryTrails.Comunication.Responses.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryTrails.Application.UseCases.Books.interfaces
{
    public interface IFindBookByIdUseCase
    {
        Task<BookDetailedResponse> Execute(string id);
    }
}
