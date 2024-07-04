using Microsoft.AspNetCore.Mvc;
using StoryTrails.Application.UseCases.Books.interfaces;
using StoryTrails.Communication.Request;
using StoryTrails.Communication.Responses.Books;
using StoryTrails.JWTAdmin.Services;


namespace StoryTrails.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        [HttpPost]
        [ProducesResponseType(typeof(BookShortResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateNewBook([FromBody] BooksJsonRequest requestBody, [FromServices] ICreateBookUseCase useCase, [FromHeader] string userToken)
        {

            var tokenAdmin = new AdminToken();
            if (tokenAdmin.ValidateToken(userToken))
            {
                var response = await useCase.Execute(requestBody, userToken);
                return Ok(response);
            }
            return Unauthorized();

        }


        [HttpGet]
        [ProducesResponseType(typeof(MultipleBooksResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> FindAllBooks([FromServices] IFindAllBooksUseCase useCase, [FromHeader] string userToken)
        {
            var tokenAdmin = new AdminToken();
            if (tokenAdmin.ValidateToken(userToken))
            {
                var response = await useCase.Execute(userToken);
                if (response is null)
                {
                    return NoContent();
                }
                return Ok(response);
            }
            return Unauthorized();
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(BookDetailedResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindBookById(string id, [FromServices] IFindBookByIdUseCase useCase, [FromHeader] string userToken)
        {
            var tokenAdmin = new AdminToken();
            if (tokenAdmin.ValidateToken(userToken))
            {
                var response = await useCase.Execute(id, userToken);

                if (response is null)
                {
                    return NotFound("Book not found");
                }
                return Ok(response);
            }
            return Unauthorized();
        }

        [HttpGet]
        [Route("collection/{id}")]
        [ProducesResponseType(typeof(BookDetailedResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ListBooksIntoCollection(string id, [FromServices] IFindBooksByCollectionUseCase useCase, [FromHeader] string userToken)
        {
            var tokenAdmin = new AdminToken();
            if (tokenAdmin.ValidateToken(userToken))
            {
                var response = await useCase.Execute(id, userToken);
                if (response.books.Count < 1)
                {
                    return NotFound("No books found on collection");
                }

                return Ok(response);
            }
            return Unauthorized();
        }

        [HttpPut]
        [Route("edit/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBookInfo(string id, [FromBody] BooksJsonRequest requestBody, [FromServices] IUpdateBookInfoUseCase useCase, [FromHeader] string userToken)
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
        [Route("/delete/{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> DeleteBookById(string id, [FromServices] IDeleteBookUseCase useCase, [FromHeader] string userToken)
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
