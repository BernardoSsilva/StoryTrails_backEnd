using Microsoft.AspNetCore.Mvc;
using StoryTrails.Application.UseCases.Collections.interfaces;
using StoryTrails.Comunication.Request;

namespace StoryTrails.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionsController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateNewCollection([FromBody] CollectionJsonRequest requestBody, [FromServices] ICreateCollectionUseCase useCase) {
            try
            {
                await useCase.Execute(requestBody);
                return Created(string.Empty, "Success");
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
