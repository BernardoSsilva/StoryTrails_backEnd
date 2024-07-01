using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoryTrails.Application.UseCases.Books.interfaces;
using StoryTrails.Comunication.Responses.Books;
using StoryTrails.Domain.Infra;


namespace StoryTrails.API.UseCases.Books
{

    public class FindAllBooksUsecase: IFindAllBooksUseCase
    {
        private readonly DatabaseSettings _context;
        private readonly IMapper _mapper;
        public FindAllBooksUsecase(IMapper mapper, DatabaseSettings context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<MultipleBooksResponse> Execute()
        {
            var response = await _context.Books.AsNoTracking().ToListAsync();


            return new MultipleBooksResponse
            {
                books = _mapper.Map<List<BookShortResponse>>(response)
            };
           
        }
    }
}
