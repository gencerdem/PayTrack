using PayTrack.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrack.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<List<TransactionDTO>> GetAllAsync();
        Task<TransactionDTO> GetByIdAsync(int id);
        Task<TransactionDTO> CreateAsync(CreateTransactionDTO dto);
        Task<List<TransactionDTO>> GetByUserIdAsync(int userId);

        Task<Dictionary<int, decimal>> GetTotalAmountPerUserAsync();
        Task<Dictionary<string, decimal>> GetTotalAmountPerTypeAsync();
        Task<List<TransactionDTO>> GetAboveThresholdAsync(decimal threshold);
    }
}
