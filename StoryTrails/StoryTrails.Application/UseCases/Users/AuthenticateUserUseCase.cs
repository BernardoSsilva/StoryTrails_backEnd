using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoryTrails.Application.UseCases.Users.interfaces;
using StoryTrails.Communication.Exceptions;
using StoryTrails.Communication.Request;
using StoryTrails.Domain.Infra;
using StoryTrails.JWTAdmin.Services;
using StoryTrails.JWTAdmin;
using StoryTrails.Communication.Responses.Users;


namespace StoryTrails.Application.UseCases.Users
{
    public class AuthenticateUserUseCase : IAuthenticateUserUseCase
    {
        private readonly Repository _repository;
        private readonly IMapper _mapper;

        public AuthenticateUserUseCase(Repository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }
        public async Task<UserAuthenticationResponse> Execute(UserAuthenticationBody requestBody)
        {
            var user = await _repository.Users.FirstOrDefaultAsync(user => user.UserLogin == requestBody.UserLogin);

            if (user == null)
            {
                throw new NotFoundError("User with this login doesn't exists");
            }
            var hashedPassword = string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(requestBody.UserPassword)).Select(s => s.ToString("x2")));

            if (hashedPassword != user.UserPassword)
            {
                throw new UnauthorizedAccessError("Access denied");
            }
            var tokenAdmin = new AdminToken();
            var newToken = tokenAdmin.Generate(new TokenPayload
            {
                UserId = user.Id,
                UserLogin = user.UserLogin
            });

            return new UserAuthenticationResponse
            {
                token = newToken,
                UserLogin = user.UserLogin
            };


        }
    }
}