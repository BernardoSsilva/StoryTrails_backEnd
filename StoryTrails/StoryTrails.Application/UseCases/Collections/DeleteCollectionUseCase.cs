using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoryTrails.Application.UseCases.Collections.interfaces;
using StoryTrails.Communication.Exceptions;
using StoryTrails.Domain.Infra;
using StoryTrails.JWTAdmin.Services;

namespace StoryTrails.Application.UseCases.Collections
{
    internal class DeleteCollectionUseCase : IDeleteCollectionUseCase
    {
        private readonly IMapper _mapper;
        private readonly Repository _repository;

        public DeleteCollectionUseCase(IMapper mapper, Repository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<bool> Execute(string id, string userToken)
        {
            try
            {
                var tokenAdmin = new AdminToken();
                var decodedToken = tokenAdmin.DecodeToken(userToken);
                var collectionToDelete = await _repository.Collections.FirstOrDefaultAsync(collection => collection.Id == id);

                if (collectionToDelete is null)
                {
                    throw new NotFoundError("Collection not found");
                }
                if (collectionToDelete.UserId != decodedToken.UserId.ToString())
                {
                    throw new UnauthorizedAccessError("Unauthorized");
                }


                _repository.Collections.Remove(collectionToDelete);
                await _repository.SaveChangesAsync();
                return true;

            }
            catch
            {
                throw new BadRequestError("bad request");
            }

        }
    }
}
