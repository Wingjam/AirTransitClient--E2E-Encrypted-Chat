using AirTransit_Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AirTransit_Core.Repositories
{
    class MessagingContext : DbContext
    {
        public MessagingContext(DbContextOptions<MessagingContext> options) : base(options)
        {
            Database.Migrate();
        }
        
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<KeySet> KeySet { get; set; }
    }
}