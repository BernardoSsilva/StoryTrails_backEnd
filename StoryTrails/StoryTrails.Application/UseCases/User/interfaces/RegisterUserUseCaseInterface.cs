using StoryTrails.Comunication.Request;
using StoryTrails.Comunication.Responses.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryTrails.Application.UseCases.Users.interfaces
{
    public interface IRegisterUserUseCase
    {
        Task<UserResponse> Execute(UserJsonRequest request);
    }
}
