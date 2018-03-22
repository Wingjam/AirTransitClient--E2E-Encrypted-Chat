using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AirTransit_Core.Repositories
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
