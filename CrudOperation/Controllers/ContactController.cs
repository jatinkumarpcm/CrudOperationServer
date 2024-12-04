using CrudOperation.Model;
using CrudOperation.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudOperation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IUserService _userService;
        public ContactController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getContactlist")]
        public async Task<IActionResult> GetResult()
        {
            var result = await _userService.GetResult();
            return Ok(new { result, StatusCode = 200 });
        }


        [HttpPost("AddContact")]
        public async Task<IActionResult> AddResult([FromBody] UserContact userContact)
        {
            var result = await _userService.AddResult(userContact);
            return Ok(new { result, Message = "Contact Added", StatusCode = 200 });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResult([FromBody] UserContact userContact, [FromRoute] int id)
        {
            if (id != userContact.Id)
                return BadRequest("Contact ID mismatch.");

            var result = await _userService.UpdateResult(id, userContact);

            return Ok(new { result = true, Message = "Contact has been Updated", StatusCode = 200 });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResult(int id)
        {
            var result = await _userService.DeleteResult(id);
            return Ok(new { result = true, Message = "Contact Deleted Succesfully", StatusCode = 200 });
        }
    }
}
