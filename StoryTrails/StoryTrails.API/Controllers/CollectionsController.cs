using Microsoft.AspNetCore.Mvc;
using StoryTrails.Application.UseCases.Collections.interfaces;
using StoryTrails.Comunication.Request;
using StoryTrails.Comunication.Responses.Collections;
using StoryTrails.JWTAdmin.Services;

namespace StoryTrails.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionsController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateNewCollection([FromBody] CollectionJsonRequest requestBody, [FromServices] ICreateCollectionUseCase useCase, [FromHeader] string userToken)
        {

            await useCase.Execute(requestBody, userToken);
            return Created(string.Empty, "Success");

        }

        [HttpGet]
        [ProducesResponseType(typeof(MultipleCollectionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ListCollections([FromServices] IGetAllCollectionsUseCase useCase, [FromHeader] string userToken)
        {
            var tokenAdmin = new AdminToken();
            if (tokenAdmin.ValidateToken(userToken))
            {
                var result = await useCase.Execute(userToken);

                if (result.Collections.Count == 0)
                {
                    return NoContent();
                }

                return Ok(result);
            }
            return Unauthorized();
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(CollectionSingleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindCollectionById(string id, [FromServices] IFindCollectionByIdUseCase useCase, [FromHeader] string userToken)
        {
            var tokenAdmin = new AdminToken();
            if (tokenAdmin.ValidateToken(userToken))
            {
                var result = await useCase.Execute(id, userToken);
                if (result is null)
                {
                    return NotFound("Not found");
                }

                return Ok(result);
            }
            return Unauthorized();
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCollection(string id, [FromBody] CollectionJsonRequest requestBody, [FromServices] IUpdateCollectionUseCase useCase, [FromHeader] string userToken)
        {

            var tokenAdmin = new AdminToken();
            if (tokenAdmin.ValidateToken(userToken))
            {

                await useCase.Execute(id, requestBody, userToken);


                return NoContent();
            }
            return Unauthorized();

        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCollection(string id, [FromServices] IDeleteCollectionUseCase useCase, [FromHeader] string userToken)
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
