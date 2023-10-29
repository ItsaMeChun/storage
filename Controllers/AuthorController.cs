using hcode.DTO;
using hcode.Entity;
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
        [ProducesResponseType(200, Type = typeof(IEnumerable<Author>))]

        public IActionResult GetAuthors()
        {
            var authors = _authorService.ListAuthors();
            return Ok(authors);
        }

        [HttpGet]
        [Route("GetAuthor/{authorId}")]
        [ProducesResponseType(200, Type = typeof(Author))]
        public IActionResult GetAuthor(int authorId)
        {
            var author = _authorService.Get(authorId);
            return Ok(author);
        }

        [HttpPost]
        [Route("CreateAuthor")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateAuthor([FromBody] AuthorDTO authorDto)
        {
            if (authorDto == null)
            {
                return BadRequest(ModelState);
            }
            var author = _authorService.FindAuthor(authorDto);
            if (author != null)
            {
                ModelState.AddModelError("", "Author already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_authorService.Add(authorDto))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPatch]
        [Route("UpdateAuthor/{authorId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateAuthor(int authorId, [FromBody] AuthorDTO updateAuthorDto)
        {
            if (updateAuthorDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_authorService.Get(authorId) == null)
            {
                return NotFound();
            }
            if (!_authorService.Update(authorId, updateAuthorDto))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully updated");
        }

        [HttpDelete("DeleteAuthor/{authorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public IActionResult DeleteAuthor(int authorId)
        {
            if (_authorService.Get(authorId) == null)
            {
                return NotFound();
            }
            if (!_authorService.Delete(authorId))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully deleted");
        }
    }
}
