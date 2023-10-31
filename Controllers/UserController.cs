﻿using AutoMapper;
using hcode.DTO;
using hcode.Entity;
using hcode.Service;
using Microsoft.AspNetCore.Mvc;

namespace hcode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _authorService;
        private readonly IMapper _mapper;
        public UserController(IUserService authorService, IMapper mapper)
        {
            this._authorService = authorService;
            this._mapper = mapper;
        }

        [HttpGet]
        [Route("GetUsers")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetAuthors()
        {
            var authors = _authorService.GetAll();
            var authorsDto = _mapper.Map<IEnumerable<UserDTO>>(authors);
            return Ok(authorsDto);
        }

        [HttpGet("{UserId}")]
        public IActionResult GetAuthor(int UserId)
        {
            var Author = _authorService.Get(UserId);
            return Ok(Author);
        }

        [HttpPost("CreateUser")]
        public IActionResult AddAuthor([FromBody] UserDTO AuthorDTO)
        {
            User author = _authorService.FindUser(AuthorDTO);
            if (author != null)
            {
                ModelState.AddModelError("AddUser", "User already exist");
                return BadRequest(ModelState);
            }
            bool result = _authorService.Add(AuthorDTO);
            if (!result)
            {
                ModelState.AddModelError("AddUser", "Something went wrong");
                return BadRequest(ModelState);
            }
            return Ok("User had been added");
        }

        [HttpPatch("UpdateUser/{UserId}")]
        public IActionResult UpdateAuthor(int UserId, [FromBody] UserDTO AuthorDto)
        {
            bool result = _authorService.Update(UserId, AuthorDto);
            if (!result)
            {
                ModelState.AddModelError("UpdateUser", "Something went wrong");
                return BadRequest(ModelState);
            }
            return Ok("User had been updated");
        }

        [HttpDelete("DeleteUser/{UserId}")]
        public IActionResult DeleteAuthor(int UserId)
        {
            bool result = _authorService.Delete(UserId);
            if (!result)
            {
                ModelState.AddModelError("DeleteUser", "Something went wrong");
                return BadRequest(ModelState);
            }
            return Ok("User had been deleted");
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLogin AuthorDTO)
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
