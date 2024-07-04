using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoryTrails.Application.UseCases.Books.interfaces;
using StoryTrails.Communication.Responses.Books;
using StoryTrails.Domain.Infra;
using StoryTrails.JWTAdmin.Services;

namespace StoryTrails.Application.UseCases.Books
{
    internal class FindBooksByCollectionUseCase : IFindBooksByCollectionUseCase
    {
        private readonly IMapper _mapper;
        private readonly Repository _repository;
        public FindBooksByCollectionUseCase(IMapper mapper, Repository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<MultipleBooksResponse> Execute(string collectionId, string userToken)
        {
            var tokenAdmin = new AdminToken();
            var decodedToken = tokenAdmin.DecodeToken(userToken);
            var booksList = await _repository.Books.ToListAsync();
            var result = booksList.Where(book => book.Collection.ToString() == collectionId.ToString() && book.User == decodedToken.UserId.ToString());
            return new MultipleBooksResponse
            {
                books = _mapper.Map<List<BookShortResponse>>(result)
            };
        }
    }
}
