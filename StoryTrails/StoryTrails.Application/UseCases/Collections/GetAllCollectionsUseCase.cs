using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoryTrails.Application.UseCases.Collections.interfaces;
using StoryTrails.Comunication.Responses.Collections;
using StoryTrails.Domain.Infra;

namespace StoryTrails.Application.UseCases.Collections
{
    internal class GetAllCollectionsUseCase : IGetAllCollectionsUseCase
    {
        private readonly IMapper _mapper;
        private readonly Repository _repository;

        public GetAllCollectionsUseCase(IMapper mapper,Repository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<MultipleCollectionResponse> Execute()
        {
            var response = await _repository.Collections.AsNoTracking().ToListAsync();

            return new MultipleCollectionResponse
            {
                Collections = _mapper.Map<List<CollectionSingleResponse>>(response)
            };
        }
    }
}
