using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoryTrails.Application.UseCases.Collections.interfaces;
using StoryTrails.Comunication.Exceptions;
using StoryTrails.Comunication.Responses.Collections;
using StoryTrails.Domain.Infra;

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
        public async Task<CollectionSingleResponse> Execute(string id)
        {
            var result = await _repository.Collections.FirstOrDefaultAsync(collection => collection.Id == id);

            return _mapper.Map<CollectionSingleResponse>(result);
        }
    }
}
