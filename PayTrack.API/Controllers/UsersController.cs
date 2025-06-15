using Microsoft.AspNetCore.Mvc;
using PayTrack.Application.DTOs;
using PayTrack.Application.Interfaces;

namespace PayTrack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _service.GetAllUsersAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id) => Ok(await _service.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Post(CreateUserDTO dto) => Ok(await _service.CreateAsync(dto));

        [HttpPut]
        public async Task<IActionResult> Put(UserDTO dto) => Ok(await _service.UpdateAsync(dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
