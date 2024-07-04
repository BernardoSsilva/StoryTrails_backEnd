using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoryTrails.Application.UseCases.Collections.interfaces;
using StoryTrails.Communication.Responses.Collections;
using StoryTrails.Domain.Infra;
using StoryTrails.JWTAdmin.Services;

namespace StoryTrails.Application.UseCases.Collections
{
    internal class GetAllCollectionsUseCase : IGetAllCollectionsUseCase
    {
        private readonly IMapper _mapper;
        private readonly Repository _repository;

        public GetAllCollectionsUseCase(IMapper mapper, Repository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<MultipleCollectionResponse> Execute(string userToken)
        {
            var tokenAdmin = new AdminToken();
            var decodedToken = tokenAdmin.DecodeToken(userToken);
            var response = await _repository.Collections.AsNoTracking().ToListAsync();

            response = response.Where(collection => collection.UserId == decodedToken.UserId.ToString()).ToList();
            return new MultipleCollectionResponse
            {
                Collections = _mapper.Map<List<CollectionSingleResponse>>(response)
            };
        }
    }
}
