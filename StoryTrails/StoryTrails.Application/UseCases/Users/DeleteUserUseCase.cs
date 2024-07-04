using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoryTrails.Application.UseCases.Users.interfaces;
using StoryTrails.Communication.Exceptions;
using StoryTrails.Domain.Infra;
using StoryTrails.JWTAdmin.Services;

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
        public async Task Execute(string id, string token)
        {

            var userToExclude = await _repository.Users.FirstOrDefaultAsync(user => user.Id == id);

            var tokenAdmin = new AdminToken();
            var decodedToken = tokenAdmin.DecodeToken(token);

            if (userToExclude == null)
            {
                throw new NotFoundError($"User with id {id} not found");
            }
            if (userToExclude.Id != decodedToken.UserId)
            {
                throw new UnauthorizedAccessError("Unauthorized");
            }

            _repository.Remove(userToExclude);
            await _repository.SaveChangesAsync();
        }
    }
}