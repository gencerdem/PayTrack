using Xunit;
using Moq;
using AutoMapper;
using PayTrack.Domain.Entities;
using PayTrack.Application.Interfaces;
using PayTrack.Infrastructure.Services;
using PayTrack.Infrastructure.Repositories;
using PayTrack.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace PayTrack.Tests.Services
{
    public class TransactionServiceTests
    {
        private readonly Mock<IRepository<Transaction>> _mockRepo;
        private readonly IMapper _mapper;
        private readonly TransactionService _service;

        public TransactionServiceTests()
        {
            _mockRepo = new Mock<IRepository<Transaction>>();

            // Mapper kur
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Transaction, TransactionDTO>();
            });
            _mapper = config.CreateMapper();

            _service = new TransactionService(_mockRepo.Object, _mapper);
        }

        [Fact]
        public async Task GetTotalAmountPerUserAsync_ShouldReturnCorrectTotals()
        {
            // Arrange
            var transactions = new List<Transaction>
            {
                new Transaction { Id = 1, UserId = "u1", Amount = 100 },
                new Transaction { Id = 2, UserId = "u1", Amount = 50 },
                new Transaction { Id = 3, UserId = "u2", Amount = 200 }
            };

            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(transactions);

            // Act
            var result = await _service.GetTotalAmountPerUserAsync();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(150, result["u1"]);
            Assert.Equal(200, result["u2"]);
        }

        [Fact]
        public async Task GetAboveThresholdAsync_ShouldReturnOnlyHighVolumeTransactions()
        {
            // Arrange
            var transactions = new List<Transaction>
                {
                    new Transaction { Id = 1, UserId = "u1", Amount = 100 },
                    new Transaction { Id = 2, UserId = "u1", Amount = 300 },
                    new Transaction { Id = 3, UserId = "u2", Amount = 500 }
                };

            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(transactions);

            // Act
            var result = await _service.GetAboveThresholdAsync(250);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, t => Assert.True(t.Amount > 250));
            Assert.Contains(result, t => t.Amount == 300);
            Assert.Contains(result, t => t.Amount == 500);
        }
    }
}
