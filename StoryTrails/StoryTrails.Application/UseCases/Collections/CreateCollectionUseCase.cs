using AutoMapper;
using StoryTrails.Application.UseCases.Collections.interfaces;
using StoryTrails.Application.Validators;
using StoryTrails.Comunication.Request;
using StoryTrails.Domain.Entities;
using StoryTrails.Domain.Infra;

namespace StoryTrails.Application.UseCases.Collections
{
    internal class CreateCollectionUseCase : ICreateCollectionUseCase
    {
        private readonly IMapper _mapper;
        private readonly DatabaseSettings _repository;

        public CreateCollectionUseCase(IMapper mapper, DatabaseSettings repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task Execute(CollectionJsonRequest request)
        {
            try
            {
                Validate(request);
                var entity = _mapper.Map<Collection>(request);
                await _repository.Collections.AddAsync(entity); 
                await _repository.SaveChangesAsync();

            }
            catch (Exception ex) 
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public void Validate(CollectionJsonRequest request)
        {
            var validator = new CollectionValidator();
            var result = validator.Validate(request);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ArgumentException(errorMessages[0]);
            }
        }
    }
}
