using Microsoft.AspNetCore.Mvc;
using StoryTrails.Application.UseCases.Users.interfaces;
using StoryTrails.Comunication.Request;
using StoryTrails.Comunication.Responses.Users;

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
            try
            {
                var result = await useCase.Execute(requestBody);
                return Created(string.Empty,result);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
                    }
        }

        [HttpGet]
        [ProducesResponseType(typeof(MultipleUsersResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ListAllUsers([FromServices] IFindAllUsersUseCase useCase)
        {
            var result =await  useCase.Execute();
            if(result.Users.Count == 0)
            {
                return NoContent();
            }
            return Ok(result);
        }
    }
}
