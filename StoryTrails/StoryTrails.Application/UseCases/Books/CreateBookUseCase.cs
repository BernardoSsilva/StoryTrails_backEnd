﻿using AutoMapper;
using StoryTrails.Application.UseCases.Books.interfaces;
using StoryTrails.Application.Validators;
using StoryTrails.Comunication.Exceptions;
using StoryTrails.Comunication.Request;
using StoryTrails.Comunication.Responses.Books;
using StoryTrails.Domain.Entities;
using StoryTrails.Domain.Infra;

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
        public async Task<BookShortResponse> Execute(BooksJsonRequest request)
        {
            try
            {
                Validate(request);
                var entity = _mapper.Map<Book>(request);

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
            if(!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new BadRequestError(errorMessages[0]);
            }
        }
    }
}
