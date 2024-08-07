﻿using Microsoft.AspNetCore.Mvc;
using StoryTrails.Application.UseCases.Users.interfaces;
using StoryTrails.Communication.Request;
using StoryTrails.Communication.Responses.Users;
using StoryTrails.JWTAdmin.Services;

namespace StoryTrails.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterNewUser([FromBody] UserJsonRequest requestBody, [FromServices] IRegisterUserUseCase useCase)
        {
            var result = await useCase.Execute(requestBody);
            return Created(string.Empty, result);
        }

        [HttpPost]
        [Route("authenticate")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AuthenticateUser([FromBody] UserAuthenticationBody requestBody, [FromServices] IAuthenticateUserUseCase useCase)
        {
            var result = await useCase.Execute(requestBody);
            return Accepted(result);
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ListAllUsers([FromServices] IFindAllUsersUseCase useCase, [FromHeader] string userToken)
        {
            if (userToken is null)
            {
                return Unauthorized();
            }

            var tokenAdmin = new AdminToken();


            if (tokenAdmin.ValidateToken(userToken))
            {

                var result = await useCase.Execute();
                if (result.Users.Count == 0)

                {
                    return NoContent();
                }
                return Ok(result);
            }

            return Unauthorized();

        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindUserById([FromServices] IFindUserByIdUseCase useCase, string id, [FromHeader] string userToken)
        {
            var tokenAdmin = new AdminToken();

            if (tokenAdmin.ValidateToken(userToken))
            {
                var result = await useCase.Execute(id);
                return Ok(result);
            }
            return Unauthorized();
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser([FromServices] IUpdateUserUseCase useCase, string id, [FromBody] UserJsonRequest requestBody, [FromHeader] string userToken)
        {
            var tokenAdmin = new AdminToken();


            if (tokenAdmin.ValidateToken(userToken))
            {
                await useCase.Execute(id, requestBody, userToken);
                return Ok();
            }
            return Unauthorized();
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser([FromServices] IDeleteUserUseCase useCase, string id, [FromHeader] string userToken)
        {
            var tokenAdmin = new AdminToken();

            if (tokenAdmin.ValidateToken(userToken))
            {
                await useCase.Execute(id, userToken);
                return Accepted();
            }
            return Unauthorized();
        }

    }
}
