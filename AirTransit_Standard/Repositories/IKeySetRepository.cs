using AirTransit_Standard.Models;

namespace AirTransit_Standard.Repositories
{
    interface IKeySetRepository
    {
        KeySet GetKeySet();
        KeySet CreateKeySet();
    }
}