using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PayTrack.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayTrack.Infrastructure
{
    public class PayTrackDbContextFactory : IDesignTimeDbContextFactory<PayTrackDbContext>
    {
        public PayTrackDbContext CreateDbContext(string[] args)
        {
            
            var optionsBuilder = new DbContextOptionsBuilder<PayTrackDbContext>();
            optionsBuilder.UseSqlServer(
                "Server=localhost\\SQLEXPRESS;Database=PayTrackDb;Trusted_Connection=True;TrustServerCertificate=True;");

            return new PayTrackDbContext(optionsBuilder.Options);
        }
    }
}
