
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoryTrails.Application.UseCases.Users.interfaces;
using StoryTrails.Communication.Exceptions;
using StoryTrails.Communication.Request;
using StoryTrails.Domain.Infra;
using StoryTrails.JWTAdmin.Services;

namespace StoryTrails.Application.UseCases.Users
{
    public class UpdateUserUseCase : IUpdateUserUseCase
    {
        private readonly IMapper _mapper;
        private readonly Repository _repository;

        public UpdateUserUseCase(IMapper mapper, Repository repository)
        {
            _mapper = mapper;
            _repository = repository;

        }
        public async Task Execute(string id, UserJsonRequest requestBody, string userToken)
        {

            var tokenAdmin = new AdminToken();
            var decodedToken = tokenAdmin.DecodeToken(userToken);

            var entityToEdit = await _repository.Users.FirstOrDefaultAsync(user => user.Id == id);

            if (entityToEdit is null)
            {
                throw new NotFoundError($"User with id {id} has not founded");
            }

            if (entityToEdit.Id != decodedToken.UserId)
            {
                throw new UnauthorizedAccessError("Unauthorized");
            }

            var entity = _mapper.Map(requestBody, entityToEdit);
            entity.UserPassword = string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(entity.UserPassword)).Select(s => s.ToString("x2")));
            _repository.Update(entity);
            await _repository.SaveChangesAsync();

        }
    }
}
