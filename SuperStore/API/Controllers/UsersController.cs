using SuperStore.Application.DTOs.Users;
using SuperStore.Application.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        public UsersController(IUserService service)
        {
            _service = service;
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var user = await _service.GetByIdAsync(id);
            if(user is null) return NotFound();
            return Ok(user);
        }

        
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<UserDto>>> GetList(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string? sortBy = null,
            [FromQuery] string? sortDir = null,
            [FromQuery] string? search = null)
            {
                if (page <= 0) page = 1;
                if (pageSize <=0) pageSize = 20;

                var users = await _service.GetListAsync(page, pageSize, sortBy, sortDir, search);
                return Ok(users);
            }


            [HttpPut("{id:int}")]
            public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
            {
                var updated = await _service.UpdateAsync(id, dto);
                if (!updated) return NotFound();
                return NoContent();
            }
           
           [HttpPost]
           public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserDto dto) 
           {
            var created = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new {id = created.Id}, created);
           }

           [HttpPatch("{id:int}")]
           public async Task<IActionResult> Patch(int id, [FromBody] PatchUserDto dto)
           {
            var patched = await _service.PatchAsync(id, dto);
            if (!patched) return NotFound();
            return NoContent();
           }

           [HttpDelete("{id:int}")]
           public async Task<IActionResult> Delete(int id)
           {
            var deleted = await _service.DeleteAsync(id);
            if(!deleted) return NotFound();
            return NoContent();
           }

            

    }
}