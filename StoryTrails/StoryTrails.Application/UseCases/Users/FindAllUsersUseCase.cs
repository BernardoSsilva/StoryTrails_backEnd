using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoryTrails.Application.UseCases.Users.interfaces;
using StoryTrails.Communication.Responses.Users;
using StoryTrails.Domain.Infra;

namespace StoryTrails.Application.UseCases.Users
{
    internal class FindAllUsersUseCase : IFindAllUsersUseCase
    {
        private readonly IMapper _mapper;
        private readonly Repository _repository;

        public FindAllUsersUseCase(IMapper mapper, Repository repository)
        {

            _mapper = mapper;
            _repository = repository;
        }
        public async Task<MultipleUsersResponse> Execute()
        {
            var result = await _repository.Users.ToListAsync();

            return new MultipleUsersResponse
            {
                Users = _mapper.Map<List<UserResponse>>(result)
            };
        }
    }
}
