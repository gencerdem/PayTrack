using AutoMapper;
using PayTrack.Application.DTOs;
using PayTrack.Application.Interfaces;
using PayTrack.Domain.Entities;
using PayTrack.Domain.Enums;
using PayTrack.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrack.Infrastructure.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionService(IRepository<Transaction> transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<List<TransactionDTO>> GetAllAsync()
        {
            var transactions = await _transactionRepository.GetAllAsync();
            return _mapper.Map<List<TransactionDTO>>(transactions);
        }

        public async Task<TransactionDTO> GetByIdAsync(int id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            return _mapper.Map<TransactionDTO>(transaction);
        }

        public async Task<TransactionDTO> CreateAsync(CreateTransactionDTO dto)
        {
            var transaction = _mapper.Map<Transaction>(dto);
            await _transactionRepository.AddAsync(transaction);
            await _transactionRepository.SaveChangesAsync();

            return _mapper.Map<TransactionDTO>(transaction);
        }

        public async Task<List<TransactionDTO>> GetByUserIdAsync(string userId)
        {
            var all = await _transactionRepository.GetAllAsync();
            var userTxns = all.Where(t => t.UserId == userId).ToList();
            return _mapper.Map<List<TransactionDTO>>(userTxns);
        }

        public async Task<Dictionary<string, decimal>> GetTotalAmountPerUserAsync()
        {
            var all = await _transactionRepository.GetAllAsync();
            return all
                .GroupBy(t => t.UserId)
                .ToDictionary(g => g.Key, g => g.Sum(t => t.Amount));
        }

        public async Task<Dictionary<string, decimal>> GetTotalAmountPerTypeAsync()
        {
            var all = await _transactionRepository.GetAllAsync();
            return all
                .GroupBy(t => t.TransactionType.ToString())
                .ToDictionary(g => g.Key, g => g.Sum(t => t.Amount));
        }

        public async Task<List<TransactionDTO>> GetAboveThresholdAsync(decimal threshold)
        {
            var all = await _transactionRepository.GetAllAsync();
            var highTxns = all.Where(t => t.Amount > threshold).ToList();
            return _mapper.Map<List<TransactionDTO>>(highTxns);
        }
    }
}
