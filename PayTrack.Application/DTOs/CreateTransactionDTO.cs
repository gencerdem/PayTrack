using PayTrack.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrack.Application.DTOs
{
    public class CreateTransactionDTO
    {
        public int UserId { get; set; }
        public decimal Amount { get; set; }

        [Range(0, 1, ErrorMessage = "TransactionType must be Debit (0) or Credit (1).")]
        public TransactionTypeEnum TransactionType { get; set; }
    }
}
