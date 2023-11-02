using hcode.DTO;
using hcode.Entity;
using hcode.Models;
using hcode.Service;
using hcode.Service.imp;
using Microsoft.AspNetCore.Mvc;

namespace hcode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SauceController : ControllerBase
    {
        private readonly ISauceService _sauceService;

        public SauceController(ISauceService sauceService)
        {
            this._sauceService = sauceService;
        }

        [HttpGet]
        [Route("GetSauces")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<IEnumerable<Sauce>>))]
        public IActionResult GetSauces()
        {
            var sauces = _sauceService.ListSauces();
            var response = new ResponseModel<IEnumerable<Sauce>>(sauces);
            response.SuccessResponse("Complete Fetch Data");
            return Ok(response);
        }

        [HttpGet]
        [Route("GetSauce/{sauceId}")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<Sauce>))]
        public IActionResult GetSauce(int sauceId)
        {
            var sauce = _sauceService.Get(sauceId);
            var response = new ResponseModel<Sauce>(sauce);
            string[] errors = { "Data response is null, sauce Id is not exist" };
            if (sauce == null)
            {
                response.ErrorResponse(errors);
                return StatusCode(404, response);
            }
            response.SuccessResponse("Complete Fetch Data");
            return Ok(response);
        }

        [HttpPost]
        [Route("CreateSauce")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<SauceDTO>))]
        [ProducesResponseType(400)]
        public IActionResult CreateAuthor([FromBody] SauceDTO sauceDto)
        {
            var response = new ResponseModel<SauceDTO>(sauceDto);
            if (sauceDto == null)
            {
                string[] errors = { "Data request is null" };
                response.ErrorResponse(errors);
                return StatusCode(406, response);
            }
            /*Need to add something to storage sauce image and complete the service stuffs*/
            return Ok(response);
        }

        // same for update
        [HttpPatch]
        [Route("UpdateAuthor/{authorId}")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<AuthorDTO>))]
        [ProducesResponseType(400)]
        public IActionResult UpdateAuthor(int authorId, [FromBody] AuthorDTO updateAuthorDto)
        {
            // the rest
            return Ok();
        }

        [HttpDelete("DeleteAuthor/{authorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(ResponseModel<SauceDTO>))]
        public IActionResult DeleteAuthor(int authorId)
        {
            var response = new ResponseModel<SauceDTO>();
            if (_sauceService.Get(authorId) == null)
            {
                string[] errors = { "Sauce does not exist" };
                response.ErrorResponse(errors);
                return StatusCode(406, response);
            }
            if (!_sauceService.Delete(authorId))
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
