using AutoMapper;
using hcode.DTO;
using hcode.Entity;
using hcode.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace hcode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            this._userService = userService;
            this._mapper = mapper;
        }

        [HttpGet]
        [Route("GetUsers")]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<string>))]
        public IActionResult GetUsers()
        {
            var users = _userService.GetAll();
            var usersDto = _mapper.Map<IEnumerable<UserDTO>>(users);
            // i update this to avoid showing someone password
            if(usersDto.IsNullOrEmpty())
            {
                ModelState.AddModelError("Get all users", "Only you is here, no one else");
                return NotFound(ModelState);
            }
            var ShowListUsers = usersDto.Select(u => new {u.UserName, u.Email});
            return Ok(ShowListUsers);
        }

        [HttpGet("{UserId}")]
        public IActionResult GetUser(int UserId)
        {
            var User = _userService.Get(UserId);
            var userDto = _mapper.Map<UserDTO>(User);
            if(userDto == null)
            {
                ModelState.AddModelError("Find User", "Can't find anyone");
                return NotFound(ModelState);
            }
            var users = new {userDto.UserName , userDto.Email};
            return Ok(users);
        }

        [HttpPost("CreateUser")]
        public IActionResult AddUser([FromBody] UserDTO UserDto)
        {
            User user = _userService.FindUser(UserDto);
            if (user != null)
            {
                ModelState.AddModelError("AddUser", "User already exist");
                return BadRequest(ModelState);
            }
            bool result = _userService.Add(UserDto);
            if (!result)
            {
                ModelState.AddModelError("AddUser", "Something went wrong");
                return BadRequest(ModelState);
            }
            return Ok("User had been added");
        }

        [HttpPatch("UpdateUser/{UserId}")]
        public IActionResult UpdateUser(int UserId, [FromBody] UserDTO UserDto)
        {
            bool result = _userService.Update(UserId, UserDto);
            if (!result)
            {
                ModelState.AddModelError("UpdateUser", "Something went wrong");
                return NotFound(ModelState);
            }
            return Ok("User had been updated");
        }

        [HttpDelete("DeleteUser/{UserId}")]
        public IActionResult DeleteUser(int UserId)
        {
            bool result = _userService.Delete(UserId);
            if (!result)
            {
                ModelState.AddModelError("DeleteUser", "Something went wrong");
                return BadRequest(ModelState);
            }
            return Ok("User had been deleted");
        }

        [HttpPost("Login")]
        public IActionResult LoginUser([FromBody] UserLogin UserDto)
        {
            var User = _userService.CheckUserLogin(UserDto);
            if (User != null)
            {
                var token = _userService.GenerateToken(User);
                return Ok(token);
            }
            return Ok(new { message = "Password or Authorname incorrect" });
        }
    };
 }
