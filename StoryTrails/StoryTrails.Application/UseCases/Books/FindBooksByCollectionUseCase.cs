using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoryTrails.Application.UseCases.Books.interfaces;
using StoryTrails.Comunication.Responses.Books;
using StoryTrails.Domain.Infra;

namespace StoryTrails.Application.UseCases.Books
{
    internal class FindBooksByCollectionUseCase : IFindBooksByCollectionUseCase
    {
        private readonly IMapper _mapper;
        private readonly DatabaseSettings _repository;
        public FindBooksByCollectionUseCase(IMapper mapper, DatabaseSettings repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<MultipleBooksResponse> Execute(string collectionId)
        {
            var booksList = await _repository.Books.ToListAsync();
            var result = booksList.Where(book => book.Collection.ToString() == collectionId.ToString());
            return new MultipleBooksResponse
            {
                books = _mapper.Map<List<BookShortResponse>>(result)
            };
        }
    }
}
