using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoryTrails.Application.UseCases.Books.interfaces;
using StoryTrails.Application.Validators;
using StoryTrails.Comunication.Request;
using StoryTrails.Domain.Infra;
using System.Diagnostics;

namespace StoryTrails.Application.UseCases.Books
{
    internal class UpdateBookUseCase : IUpdateBookInfoUseCase
    {
        private readonly DatabaseSettings _repository;
        private readonly IMapper _mapper;

        public UpdateBookUseCase(DatabaseSettings repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task Execute(string id, BooksJsonRequest requestBody)
        {
            var bookToEdit = await _repository.Books.FirstOrDefaultAsync(book => book.Id == id);


            Validate(requestBody);


            if (bookToEdit == null) 
            {
                throw new Exception("not found");
            }

            _mapper.Map(requestBody, bookToEdit);
            _repository.Update(bookToEdit);
            await _repository.SaveChangesAsync();
        }

        public void Validate(BooksJsonRequest request)
        {
            var validator = new BookValidator();
            var result = validator.Validate(request);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ArgumentException(errorMessages[0]);
            }
        }
    }
}
