using AirTransit_Core.Models;

namespace AirTransit_Core.Repositories
{
    interface IKeySetRepository
    {
        KeySet GetOrCreateKeySet();
        KeySet GetOrCreateKeySet(string phoneNumber);
    }
}