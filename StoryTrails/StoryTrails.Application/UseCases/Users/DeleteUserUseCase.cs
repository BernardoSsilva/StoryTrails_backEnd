using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoryTrails.Application.UseCases.Users.interfaces;
using StoryTrails.Comunication.Exceptions;
using StoryTrails.Domain.Infra;

namespace StoryTrails.Application.UseCases.Users
{
    public class DeleteUserUseCase : IDeleteUserUseCase
    {
        private readonly Repository _repository;
        private readonly IMapper _mapper;
        public DeleteUserUseCase(Repository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task Execute(string id)
        {

            var userToExclude = await _repository.Users.FirstOrDefaultAsync(user => user.Id == id);
            if (userToExclude == null)
            {
                throw new NotFoundError($"User with id {id} not found");
            }
            _repository.Remove(userToExclude);
            await _repository.SaveChangesAsync();
        }
    }
}