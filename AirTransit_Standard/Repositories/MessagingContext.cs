using AirTransit_Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AirTransit_Core.Repositories
{
    internal class MessagingContext : DbContext
    {
        public MessagingContext(DbContextOptions<MessagingContext> options) : base(options)
        {
            SQLitePCL.Batteries.Init();
            Database.Migrate();
        }
        
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<KeySet> KeySet { get; set; }
    }
}