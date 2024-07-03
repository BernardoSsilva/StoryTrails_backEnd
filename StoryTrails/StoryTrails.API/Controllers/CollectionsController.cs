using Microsoft.AspNetCore.Mvc;
using StoryTrails.Application.UseCases.Collections.interfaces;
using StoryTrails.Comunication.Request;
using StoryTrails.Comunication.Responses.Collections;

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

        [HttpGet]
        [ProducesResponseType(typeof(MultipleCollectionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ListCollections([FromServices] IGetAllCollectionsUseCase useCase)
        {
            var result = await useCase.Execute();

            if(result.Collections.Count == 0) {
                return NoContent();
            }

            return Ok(result);
        }

    }
}
