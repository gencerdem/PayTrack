using PayTrack.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrack.Application.DTOs
{
    public class CreateTransactionDTO
    {
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public TransactionTypeEnum TransactionType { get; set; }
    }
}
