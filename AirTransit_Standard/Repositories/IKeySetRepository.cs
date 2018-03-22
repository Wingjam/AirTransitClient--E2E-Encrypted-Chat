using AirTransit_Core.Models;

namespace AirTransit_Core.Repositories
{
    internal interface IKeySetRepository
    {
        KeySet GetKeySet();
        KeySet CreateKeySet();
    }
}