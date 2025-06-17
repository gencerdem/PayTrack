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

        /// <summary>
        /// Gets all active users.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _service.GetAllUsersAsync());

        /// <summary>
        /// Gets a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) => Ok(await _service.GetByIdAsync(id));

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="dto">The user creation data.</param>
        [HttpPost]
        public async Task<IActionResult> Post(CreateUserDTO dto) => Ok(await _service.CreateAsync(dto));

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="dto">The user data to update.</param>
        [HttpPut]
        public async Task<IActionResult> Put(UserDTO dto) => Ok(await _service.UpdateAsync(dto));

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
