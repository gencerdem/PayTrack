﻿using PayTrack.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrack.Domain.Entities
{
    public class User:BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
