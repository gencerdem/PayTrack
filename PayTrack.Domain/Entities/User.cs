using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrack.Domain.Entities
{
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
            Transactions = new List<Transaction>();
        }
        public string Id { get; set; }
        public string Name { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
