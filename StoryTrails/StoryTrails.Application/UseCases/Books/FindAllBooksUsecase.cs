using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoryTrails.Application.UseCases.Books.interfaces;
using StoryTrails.Communication.Responses.Books;
using StoryTrails.Domain.Infra;
using StoryTrails.JWTAdmin.Services;


namespace StoryTrails.API.UseCases.Books
{

    public class FindAllBooksUseCase : IFindAllBooksUseCase
    {
        private readonly Repository _context;
        private readonly IMapper _mapper;
        public FindAllBooksUseCase(IMapper mapper, Repository context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<MultipleBooksResponse> Execute(string userToken)
        {
            var tokenAdmin = new AdminToken();
            var decodedToken = tokenAdmin.DecodeToken(userToken);
            var response = await _context.Books.AsNoTracking().ToListAsync();

            response = response.Where(book => book.User == decodedToken.UserId.ToString()).ToList();

            return new MultipleBooksResponse
            {
                books = _mapper.Map<List<BookShortResponse>>(response)
            };

        }
    }
}
