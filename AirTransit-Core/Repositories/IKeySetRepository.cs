using AirTransit_Core.Models;

namespace AirTransit_Core.Repositories
{
    interface IKeySetRepository
    {
        KeySet GetKeySet(string phoneNumber);
    }
}