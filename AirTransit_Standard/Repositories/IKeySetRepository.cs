using AirTransit_Standard.Models;

namespace AirTransit_Standard.Repositories
{
    internal interface IKeySetRepository
    {
        KeySet GetKeySet();
        KeySet CreateKeySet();
    }
}