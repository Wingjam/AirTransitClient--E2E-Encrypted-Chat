using AirTransit_Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AirTransit_Core.Repositories
{
    public class MessagingContext : DbContext
    {
        public MessagingContext(DbContextOptions<MessagingContext> options) : base(options)
        {
        }
        
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Contact> KeySet { get; set; }
    }
}