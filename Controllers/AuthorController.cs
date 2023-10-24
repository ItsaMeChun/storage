using hcode.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hcode.Controllers
{
    [Route("api/author")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        [HttpGet(Name = "/")]
        public ActionResult<String> GetGretting()
        {
            return Ok("Help");
        }

        /*[HttpGet(Name = "get-all")]
        public ActionResult<List<AuthorModel>> GetAllAuthor()
        {
            //List<AuthorModel> authors = _context.Authors.ToList();
            var authors = "yes this is all";
            return Ok(authors);
        }*/
    };
 }
