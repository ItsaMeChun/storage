using AutoMapper;
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
        private readonly IMapper _mapper;
        public AuthorController(IAuthorService authorService, IMapper mapper)
        {
            this._authorService = authorService;
            this._mapper = mapper;
        }

        [HttpGet]
        [Route("GetAuthors")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Author>))]
        public IActionResult GetAuthors()
        {
            var authors = _authorService.GetAll();
            var authorsDto = _mapper.Map<IEnumerable<AuthorDTO>>(authors);
            return Ok(authorsDto);
        }

        [HttpGet("{AuthorId}")]
        public IActionResult GetAuthor(int AuthorId)
        {
            var Author = _authorService.Get(AuthorId);
            return Ok(Author);
        }

        [HttpPost("CreateAuthor")]
        public IActionResult AddAuthor([FromBody] AuthorDTO AuthorDTO)
        {
            Author author = _authorService.FindUser(AuthorDTO);
            if (author != null)
            {
                ModelState.AddModelError("AddAuthor", "Author already exist");
                return BadRequest(ModelState);
            }
            Author AuthorMap = _mapper.Map<Author>(AuthorDTO);
            bool result = _authorService.Add(AuthorMap);
            if (!result)
            {
                ModelState.AddModelError("AddAuthor", "Something went wrong");
                return BadRequest(ModelState);
            }
            return Ok("Author had been added");
        }

        [HttpPatch("UpdateAuthor/{AuthorId}")]
        public IActionResult UpdateAuthor(int AuthorId, [FromBody] AuthorDTO AuthorDto)
        {
            bool result = _authorService.Update(AuthorId, AuthorDto);
            if (!result)
            {
                ModelState.AddModelError("UpdateAuthor", "Something went wrong");
                return BadRequest(ModelState);
            }
            return Ok("Author had been updated");
        }

        [HttpDelete("DeleteAuthor/{AuthorId}")]
        public IActionResult DeleteAuthor(int AuthorId)
        {
            bool result = _authorService.Delete(AuthorId);
            if (!result)
            {
                ModelState.AddModelError("DeleteAuthor", "Something went wrong");
                return BadRequest(ModelState);
            }
            return Ok("Author had been deleted");
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] AuthorLogin AuthorDTO)
        {
            var Author = _authorService.CheckUserLogin(AuthorDTO);
            if (Author != null)
            {
                var token = _authorService.GenerateToken(Author);
                return Ok(token);
            }
            return Ok(new { message = "Password or Authorname incorrect" });
        }
    };
 }
