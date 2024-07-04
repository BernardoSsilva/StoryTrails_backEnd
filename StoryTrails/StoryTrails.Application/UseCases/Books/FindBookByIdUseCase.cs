using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoryTrails.Application.UseCases.Books.interfaces;
using StoryTrails.Communication.Responses.Books;
using StoryTrails.Domain.Infra;
using StoryTrails.JWTAdmin.Services;

namespace StoryTrails.Application.UseCases.Books
{
    public class FindBookByIdUseCase : IFindBookByIdUseCase
    {
        private readonly IMapper _mapper;
        private readonly Repository _repository;

        public FindBookByIdUseCase(Repository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BookDetailedResponse> Execute(string id, string userToken)
        {
            var tokenAdmin = new AdminToken();
            var decodedToken = tokenAdmin.DecodeToken(userToken);

            var response = await _repository.Books.AsNoTracking().FirstOrDefaultAsync(book => book.Id == id && book.User == decodedToken.UserId.ToString());
            return _mapper.Map<BookDetailedResponse>(response);
        }
    }
}
