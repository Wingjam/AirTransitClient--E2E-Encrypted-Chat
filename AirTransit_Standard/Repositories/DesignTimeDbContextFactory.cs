using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirTransit_Standard.Repositories
{
    class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MessagingContext>
    {
        public MessagingContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MessagingContext>();
            optionsBuilder.UseSqlite("Data Source=airtransit.db");
            return new MessagingContext(optionsBuilder.Options);
        }
    }
}
