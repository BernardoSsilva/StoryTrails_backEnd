using AutoMapper;
using StoryTrails.Application.UseCases.Users.interfaces;
using StoryTrails.Application.Validators;
using StoryTrails.Comunication.Request;
using StoryTrails.Comunication.Responses.Users;
using StoryTrails.Domain.Infra;
using StoryTrails.Domain.Entities;

using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using StoryTrails.Comunication.Exceptions;

namespace StoryTrails.Application.UseCases.Users
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IMapper _mapper;
        private readonly Repository _repository;

        public RegisterUserUseCase(Repository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<UserResponse> Execute(UserJsonRequest request)
        {
            Validate(request);

            var entity = _mapper.Map<User>(request);

            HashAlgorithm algorithm = SHA256.Create();

            var userAlreadyExists = await _repository.Users.FirstOrDefaultAsync(user => user.UserLogin == request.UserLogin);

            if (userAlreadyExists != null)
            {
                throw new ConflictError("User already exists");
            }

            entity.UserPassword = string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(entity.UserPassword)).Select(s => s.ToString("x2")));
            await _repository.Users.AddAsync(entity);
            await _repository.SaveChangesAsync();
            return _mapper.Map<UserResponse>(entity);
        }

        public void Validate(UserJsonRequest request) {

            var validator = new UserValidator();
            var result = validator.Validate(request);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.ToList();
                throw new BadRequestError(errorMessages[0].ToString());
            }
        }
    }
}
