using Microsoft.AspNetCore.Mvc;
using StoryTrails.Application.UseCases.Books.interfaces;
using StoryTrails.Comunication.Responses.Books;


namespace StoryTrails.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<MultipleBooksResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> FindAllBooks ([FromServices] IFindAllBooksUseCase useCase)
        {
            var response = await useCase.Execute();
            if(response is null)
            {
                return NoContent();
            }
            return Ok(response);
        }

    }
}
