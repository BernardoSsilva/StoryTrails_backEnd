﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoryTrails.Application.UseCases.Books.interfaces;
using StoryTrails.Comunication.Exceptions;
using StoryTrails.Domain.Infra;

namespace StoryTrails.Application.UseCases.Books
{
    public class DeleteBookUseCase: IDeleteBookUseCase
    {
        private readonly IMapper _mapper;
        private readonly Repository _repository;
        public DeleteBookUseCase(Repository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task Execute(string id)
        {
          var bookToDelete = await _repository.Books.FirstOrDefaultAsync(book => book.Id == id);
            if (bookToDelete is null)
            {
                throw new NotFoundError("Book not found");           
            }
            _repository.Books.Remove(bookToDelete);
            _repository.SaveChanges();
        }
    }
    
}
