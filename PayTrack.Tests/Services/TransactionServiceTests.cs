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
        private readonly Mock<IRepository<User>> _mockUserRepo;

        private readonly IMapper _mapper;
        private readonly TransactionService _service;

        public TransactionServiceTests()
        {
            _mockRepo = new Mock<IRepository<Transaction>>();
            _mockUserRepo = new Mock<IRepository<User>>();

            // Mapper kur
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Transaction, TransactionDTO>();
            });
            _mapper = config.CreateMapper();

            _service = new TransactionService(_mockRepo.Object, _mockUserRepo.Object, _mapper);
        }

        [Fact]
        public async Task GetTotalAmountPerUserAsync_ShouldReturnCorrectTotals()
        {
            // Arrange
            var transactions = new List<Transaction>
            {
                new Transaction { Id = 1, UserId = 1, Amount = 100 },
                new Transaction { Id = 2, UserId = 2, Amount = 50 },
                new Transaction { Id = 3, UserId = 3, Amount = 200 }
            };
            _mockRepo.Setup(r => r.GetAllAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Transaction, bool>>>()))
         .ReturnsAsync(transactions);
            //_mockRepo.Setup(r => r.GetAllAsync(x => x.IsActive)).ReturnsAsync(transactions);

            // Act
            var result = await _service.GetTotalAmountPerUserAsync();

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal(100, result[1]);
            Assert.Equal(50, result[2]);
            Assert.Equal(200, result[3]);
        }

        [Fact]
        public async Task GetAboveThresholdAsync_ShouldReturnOnlyHighVolumeTransactions()
        {
            // Arrange
            var transactions = new List<Transaction>
                {
                    new Transaction { Id = 1, UserId = 1, Amount = 100 },
                    new Transaction { Id = 2, UserId = 1, Amount = 300 },
                    new Transaction { Id = 3, UserId = 2, Amount = 500 }
                };

            //_mockRepo.Setup(r => r.GetAllAsync(x => x.IsActive)).ReturnsAsync(transactions);
            _mockRepo.Setup(r => r.GetAllAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Transaction, bool>>>()))
         .ReturnsAsync(transactions);

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
