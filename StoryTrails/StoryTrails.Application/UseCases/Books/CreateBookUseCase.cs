using AutoMapper;
using StoryTrails.Application.UseCases.Books.interfaces;
using StoryTrails.Application.Validators;
using StoryTrails.Communication.Exceptions;
using StoryTrails.Communication.Request;
using StoryTrails.Communication.Responses.Books;
using StoryTrails.Domain.Entities;
using StoryTrails.Domain.Infra;
using StoryTrails.JWTAdmin.Services;

namespace StoryTrails.Application.UseCases.Books
{
    internal class CreateBookUseCase : ICreateBookUseCase
    {
        private readonly IMapper _mapper;
        private readonly Repository _repository;
        public CreateBookUseCase(Repository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<BookShortResponse> Execute(BooksJsonRequest request, string userToken)
        {
            try
            {
                var tokenAdmin = new AdminToken();
                var decodedToken = tokenAdmin.DecodeToken(userToken);

                Validate(request);
                var entity = _mapper.Map<Book>(request);
                entity.User = decodedToken.UserId.ToString();
                await _repository.Books.AddAsync(entity);
                await _repository.SaveChangesAsync();
                return new BookShortResponse
                {
                    Id = entity.Id,
                    BookName = request.BookName,
                    Concluded = request.Concluded,
                    PagesAmount = request.PagesAmount,
                };
            }
            catch (Exception ex)
            {
                throw new BadRequestError(ex.Message);
            }

        }

        public void Validate(BooksJsonRequest request)
        {
            var validator = new BookValidator();
            var result = validator.Validate(request);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new BadRequestError(errorMessages[0]);
            }
        }
    }
}
