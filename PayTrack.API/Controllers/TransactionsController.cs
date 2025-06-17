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

        /// <summary>
        /// Gets all active transactions.
        /// </summary>
        /// <returns>List of transactions.</returns>
        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());

        /// <summary>
        /// Gets a transaction by ID.
        /// </summary>
        /// <param name="id">The transaction ID.</param>
        /// <returns>Transaction data.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) => Ok(await _service.GetByIdAsync(id));

        /// <summary>
        /// Gets all transactions for a specific user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>List of transactions for the user.</returns>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId) => Ok(await _service.GetByUserIdAsync(userId));

        /// <summary>
        /// Creates a new transaction.
        /// </summary>
        /// <param name="dto">Transaction data to create.</param>
        /// <returns>The created transaction.</returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateTransactionDTO dto) => Ok(await _service.CreateAsync(dto));

        /// <summary>
        /// Gets total transaction amount per user.
        /// </summary>
        /// <returns>A dictionary of userId and total amount.</returns>
        [HttpGet("summary/by-user")]
        public async Task<IActionResult> TotalByUser() => Ok(await _service.GetTotalAmountPerUserAsync());

        /// <summary>
        /// Gets total transaction amount per transaction type.
        /// </summary>
        /// <returns>A dictionary of transaction type and total amount.</returns>
        [HttpGet("summary/by-type")]
        public async Task<IActionResult> TotalByType() => Ok(await _service.GetTotalAmountPerTypeAsync());

        /// <summary>
        /// Gets transactions above a certain threshold.
        /// </summary>
        /// <param name="threshold">The threshold amount.</param>
        /// <returns>List of transactions where amount exceeds the threshold.</returns>
        [HttpGet("summary/above/{threshold}")]
        public async Task<IActionResult> HighVolume(decimal threshold) => Ok(await _service.GetAboveThresholdAsync(threshold));
    }
}
