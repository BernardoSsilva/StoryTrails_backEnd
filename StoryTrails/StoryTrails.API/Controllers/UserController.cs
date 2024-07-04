using Microsoft.AspNetCore.Mvc;
using StoryTrails.Application.UseCases.Users.interfaces;
using StoryTrails.Comunication.Request;

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
    }
}
