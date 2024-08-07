﻿using StoryTrails.Communication.Responses.Users;

namespace StoryTrails.Application.UseCases.Users.interfaces
{
    public interface IFindAllUsersUseCase
    {
        Task<MultipleUsersResponse> Execute();
    }
}
