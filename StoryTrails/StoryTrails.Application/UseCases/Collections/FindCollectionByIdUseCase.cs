using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoryTrails.Application.UseCases.Collections.interfaces;
using StoryTrails.Communication.Exceptions;
using StoryTrails.Communication.Responses.Collections;
using StoryTrails.Domain.Infra;
using StoryTrails.JWTAdmin.Services;

namespace StoryTrails.Application.UseCases.Collections
{
    internal class FindCollectionByIdUseCase : IFindCollectionByIdUseCase
    {
        private readonly Repository _repository;
        private readonly IMapper _mapper;

        public FindCollectionByIdUseCase(Repository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<CollectionSingleResponse> Execute(string id, string userToken)
        {
            var tokenAdmin = new AdminToken();
            var decodedToken = tokenAdmin.DecodeToken(userToken);
            var result = await _repository.Collections.FirstOrDefaultAsync(collection => collection.Id == id && collection.UserId == decodedToken.UserId.ToString());

            return _mapper.Map<CollectionSingleResponse>(result);
        }
    }
}
