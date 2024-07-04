using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoryTrails.Application.UseCases.Collections.interfaces;
using StoryTrails.Application.Validators;
using StoryTrails.Comunication.Exceptions;
using StoryTrails.Comunication.Request;
using StoryTrails.Domain.Infra;
using StoryTrails.JWTAdmin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryTrails.Application.UseCases.Collections
{
    internal class UpdateCollectionUseCase : IUpdateCollectionUseCase
    {
        private readonly IMapper _mapper;
        private readonly Repository _repository;

        public UpdateCollectionUseCase(Repository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<bool> Execute(string id, CollectionJsonRequest requestBody, string userToken)
        {
            try
            {
                var tokenAdmin = new AdminToken();
                var decodedToken = tokenAdmin.DecodeToken(userToken);
                Validate(requestBody);
                var entityToUpdate = await _repository.Collections.FirstOrDefaultAsync(collection => collection.Id == id);
                if (entityToUpdate is null)
                {
                    throw new NotFoundError("Collection Not found");
                }
                if (entityToUpdate.UserId != decodedToken.UserId.ToString())
                {
                    throw new UnauthorizedAccessError("Unauthorized");
                }

                var newData = _mapper.Map(requestBody, entityToUpdate);
                _repository.Update(newData);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new BadRequestError(ex.Message);
            }
        }

        private void Validate(CollectionJsonRequest requestBody)
        {
            var validator = new CollectionValidator();
            var result = validator.Validate(requestBody);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

                throw new BadRequestError(errorMessages[0]);
            }
        }
    }
}
