using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoryTrails.Application.UseCases.Books.interfaces;
using StoryTrails.Comunication.Responses.Books;
using StoryTrails.Domain.Infra;

namespace StoryTrails.Application.UseCases.Books
{
    public class FindBookByIdUseCase: IFindBookByIdUseCase
    {
        private readonly IMapper _mapper;
        private readonly Repository _repository;

        public FindBookByIdUseCase(Repository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BookDetailedResponse> Execute(string id)
        {
            var response = await _repository.Books.AsNoTracking().FirstOrDefaultAsync(book => book.Id == id);
            return _mapper.Map<BookDetailedResponse>(response);
        }
    }
}
