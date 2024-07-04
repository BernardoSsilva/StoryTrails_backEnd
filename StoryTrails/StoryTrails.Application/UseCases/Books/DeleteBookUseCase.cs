using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoryTrails.Application.UseCases.Books.interfaces;
using StoryTrails.Communication.Exceptions;
using StoryTrails.Domain.Infra;
using StoryTrails.JWTAdmin.Services;

namespace StoryTrails.Application.UseCases.Books
{
    public class DeleteBookUseCase : IDeleteBookUseCase
    {
        private readonly IMapper _mapper;
        private readonly Repository _repository;
        public DeleteBookUseCase(Repository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task Execute(string id, string userToken)
        {
            var tokenAdmin = new AdminToken();
            var decodedToken = tokenAdmin.DecodeToken(userToken);
            var bookToDelete = await _repository.Books.FirstOrDefaultAsync(book => book.Id == id && book.User == decodedToken.UserId.ToString());
            if (bookToDelete is null)
            {
                throw new NotFoundError("Book not found");
            }
            _repository.Books.Remove(bookToDelete);
            _repository.SaveChanges();
        }
    }

}
