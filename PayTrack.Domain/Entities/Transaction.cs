using PayTrack.Domain.Common;
using PayTrack.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrack.Domain.Entities
{
    public class Transaction:BaseEntity
    {
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        
        public User User { get; set; }

        public TransactionTypeEnum TransactionType { get; set; }
    }
}
