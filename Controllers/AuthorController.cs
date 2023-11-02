using hcode.DTO;
using hcode.Entity;
using hcode.Models;
using hcode.Service;
using Microsoft.AspNetCore.Mvc;

namespace hcode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            this._authorService = authorService;
        }

        [HttpGet]
        [Route("GetAuthors")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<IEnumerable<Author>>))]

        public IActionResult GetAuthors()
        {
            var authors = _authorService.ListAuthors();
            var response = new ResponseModel<IEnumerable<Author>>(authors);
            response.SuccessResponse("Complete Fetch Data");
            return Ok(response);
        }

        [HttpGet]
        [Route("GetAuthor/{authorId}")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<Author>))]
        public IActionResult GetAuthor(int authorId)
        {
            var author = _authorService.Get(authorId);
            var response = new ResponseModel<Author>(author);
            string[] errors = { "Data response is null, user Id is not exist" };
            if (author == null)
            {
                response.ErrorResponse(errors);
                return StatusCode(404, response);
            }
            response.SuccessResponse("Complete Fetch Data");
            return Ok(response);
        }

        [HttpPost]
        [Route("CreateAuthor")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<AuthorDTO>))]
        [ProducesResponseType(400)]
        public IActionResult CreateAuthor([FromBody] AuthorDTO authorDto)
        {
            var response = new ResponseModel<AuthorDTO>(authorDto);
            if (authorDto == null)
            {
                string[] errors = { "Data request is null" };
                response.ErrorResponse(errors);
                return StatusCode(406, response);
            }
            var author = _authorService.FindAuthor(authorDto);
            if (author != null)
            {
                string[] errors = { "Author already exist" };
                response.ErrorResponse(errors);
                return StatusCode(422, response);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_authorService.Add(authorDto))
            {
                string[] errors = { "Something went wrong while saving" };
                response.ErrorResponse(errors);
                return StatusCode(500, response);
            }
            response.SuccessResponse("Successfully created");
            return Ok(response);
        }

        [HttpPatch]
        [Route("UpdateAuthor/{authorId}")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<AuthorDTO>))]
        [ProducesResponseType(400)]
        public IActionResult UpdateAuthor(int authorId, [FromBody] AuthorDTO updateAuthorDto)
        {
            var response = new ResponseModel<AuthorDTO>(updateAuthorDto);
            if (updateAuthorDto == null)
            {
                string[] errors = { "Data request is null" };
                response.ErrorResponse(errors);
                return StatusCode(406, response);
            }
            if (_authorService.Get(authorId) == null)
            {
                string[] errors = { "Author does not exist" };
                response.ErrorResponse(errors);
                return StatusCode(406, response);
            }
            if (!_authorService.Update(authorId, updateAuthorDto))
            {
                string[] errors = { "Something went wrong while updating" };
                response.ErrorResponse(errors);
                return StatusCode(500, response);
            }
            response.SuccessResponse("Successfully updated");
            return Ok(response);
        }

        [HttpDelete("DeleteAuthor/{authorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(ResponseModel<AuthorDTO>))]
        public IActionResult DeleteAuthor(int authorId)
        {
            var response = new ResponseModel<AuthorDTO>();
            if (_authorService.Get(authorId) == null)
            {
                string[] errors = { "Author does not exist" };
                response.ErrorResponse(errors);
                return StatusCode(406, response);
            }
            if (!_authorService.Delete(authorId))
            {
                string[] errors = { "Something went wrong while deleting" };
                response.ErrorResponse(errors);
                return StatusCode(500, response);
            }
            response.SuccessResponse("Successfully deleted");
            return Ok(response);
        }
    }
}
