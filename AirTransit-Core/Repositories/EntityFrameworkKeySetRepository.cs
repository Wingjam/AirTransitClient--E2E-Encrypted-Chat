using System.Linq;
using AirTransit_Core.Models;

namespace AirTransit_Core.Repositories
{
    class EntityFrameworkKeySetRepository : EntityFrameworkRepository, IKeySetRepository
    {
        public EntityFrameworkKeySetRepository(MessagingContext messagingContext) : base(messagingContext) { }
        public KeySet GetKeySet(string phoneNumber)
        {
            return this.MessagingContext.KeySet.SingleOrDefault(ks => ks.PhoneNumber == phoneNumber);
        }
    }
}