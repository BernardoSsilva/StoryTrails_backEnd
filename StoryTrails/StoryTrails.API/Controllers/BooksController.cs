using Microsoft.AspNetCore.Mvc;
using StoryTrails.Application.UseCases.Books.interfaces;
using StoryTrails.Comunication.Request;
using StoryTrails.Comunication.Responses.Books;


namespace StoryTrails.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        [HttpPost]
        [ProducesResponseType(typeof(BookShortResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateNewBook([FromBody] BooksJsonRequest requestBody, [FromServices] ICreateBookUseCase useCase)
        {
            try
            {

            var response = await useCase.Execute(requestBody);
            return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [ProducesResponseType(typeof(MultipleBooksResponse), StatusCodes.Status200OK)]
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

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(BookDetailedResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindBookById( string id, [FromServices] IFindBookByIdUseCase useCase)
        {
            var response = await useCase.Execute(id);

            if(response is null)
            {
                return NotFound("not found");
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("collection/{id}")]
        [ProducesResponseType(typeof(BookDetailedResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ListBooksIntoCollection(string id, [FromServices] IFindBooksByCollectionUseCase useCase)
        {
            var response = await useCase.Execute(id);
            if (response.books.Count<1 )
            {
                return NotFound("No books found on collection");
            }
           
            return Ok(response);
        }
    }
}
