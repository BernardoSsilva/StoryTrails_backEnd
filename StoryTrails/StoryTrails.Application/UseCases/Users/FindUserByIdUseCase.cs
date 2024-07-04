using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoryTrails.Application.UseCases.Users.interfaces;
using StoryTrails.Comunication.Exceptions;
using StoryTrails.Comunication.Responses.Users;
using StoryTrails.Domain.Infra;

namespace StoryTrails.Application.UseCases.Users
{
    internal class FindUserByIdUseCase : IFindUserByIdUseCase
    {
        private readonly IMapper _mapper;
        private readonly Repository _repository;

        public FindUserByIdUseCase(Repository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<UserResponse> Execute(string id)
        {
            var result = await _repository.Users.FirstOrDefaultAsync(user => user.Id == id);
            if(result == null)
            {
                throw new NotFoundError("User not found");
            }
            return _mapper.Map<UserResponse>(result);
        }
    }
}
