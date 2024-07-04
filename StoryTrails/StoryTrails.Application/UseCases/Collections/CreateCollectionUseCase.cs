using AutoMapper;
using StoryTrails.Application.UseCases.Collections.interfaces;
using StoryTrails.Application.Validators;
using StoryTrails.Comunication.Exceptions;
using StoryTrails.Comunication.Request;
using StoryTrails.Domain.Entities;
using StoryTrails.Domain.Infra;
using StoryTrails.JWTAdmin.Services;

namespace StoryTrails.Application.UseCases.Collections
{
    internal class CreateCollectionUseCase : ICreateCollectionUseCase
    {
        private readonly IMapper _mapper;
        private readonly Repository _repository;

        public CreateCollectionUseCase(IMapper mapper, Repository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task Execute(CollectionJsonRequest request, string userToken)
        {
            var tokenAdmin = new AdminToken();
            var decodedToken = tokenAdmin.DecodeToken(userToken);

            Validate(request);
            var entity = _mapper.Map<Collection>(request);
            entity.UserId = decodedToken.UserId.ToString();
            await _repository.Collections.AddAsync(entity);
            await _repository.SaveChangesAsync();


        }

        public void Validate(CollectionJsonRequest request)
        {
            var validator = new CollectionValidator();
            var result = validator.Validate(request);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new BadRequestError(errorMessages[0]);
            }
        }
    }
}
