using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoryTrails.Application.UseCases.Collections.interfaces;
using StoryTrails.Domain.Infra;

namespace StoryTrails.Application.UseCases.Collections
{
    internal class DeleteCollectionUseCase : IDeleteCollectionUseCase
    {
        private readonly IMapper _mapper;
        private readonly DatabaseSettings _repository;

        public DeleteCollectionUseCase(IMapper mapper, DatabaseSettings repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<bool> Execute(string id)
        {
            try
            {
                var collectionToDelete = await _repository.Collections.FirstOrDefaultAsync(collection => collection.Id == id);

                if (collectionToDelete is null) {
                    return false;
                }

                _repository.Collections.Remove(collectionToDelete);
                await _repository.SaveChangesAsync();
                return true;

            }
            catch (Exception ex) 
            {
                throw new Exception("bad request");
            }

        }
    }
}
