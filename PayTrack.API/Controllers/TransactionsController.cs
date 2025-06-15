using Microsoft.AspNetCore.Mvc;
using PayTrack.Application.DTOs;
using PayTrack.Application.Interfaces;

namespace PayTrack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _service;

        public TransactionsController(ITransactionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) => Ok(await _service.GetByIdAsync(id));

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(string userId) => Ok(await _service.GetByUserIdAsync(userId));

        [HttpPost]
        public async Task<IActionResult> Post(CreateTransactionDTO dto) => Ok(await _service.CreateAsync(dto));

        [HttpGet("summary/by-user")]
        public async Task<IActionResult> TotalByUser() => Ok(await _service.GetTotalAmountPerUserAsync());

        [HttpGet("summary/by-type")]
        public async Task<IActionResult> TotalByType() => Ok(await _service.GetTotalAmountPerTypeAsync());

        [HttpGet("summary/above/{threshold}")]
        public async Task<IActionResult> HighVolume(decimal threshold) => Ok(await _service.GetAboveThresholdAsync(threshold));
    }
}
