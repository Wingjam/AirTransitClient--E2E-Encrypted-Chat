using System.Linq;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Threading;
using AirTransit_Core.Models;
using Newtonsoft.Json;

namespace AirTransit_Core.Repositories
{
    class EntityFrameworkKeySetRepository : EntityFrameworkRepository, IKeySetRepository
    {
        public EntityFrameworkKeySetRepository(MessagingContext messagingContext) : base(messagingContext) { }
        public KeySet GetOrCreateKeySet(string phoneNumber)
        {
            var keySet = this.MessagingContext.KeySet.SingleOrDefault(ks => ks.PhoneNumber == phoneNumber);
            if (keySet == null)
            {
                keySet = CreateRSAKeyPair(phoneNumber);
                MessagingContext.KeySet.Add(keySet);
            }

            return keySet;
        }

        private KeySet CreateRSAKeyPair(string phoneNumber)
        {
            using (var rsa = RSA.Create())
            {
                var rsaParameters = rsa.ExportParameters(false);
                var publicKey = JsonConvert.SerializeObject(rsaParameters);
                
                rsaParameters = rsa.ExportParameters(true);
                var privateKey = JsonConvert.SerializeObject(rsaParameters);
                return new KeySet(phoneNumber, publicKey, privateKey);
            }
        }
    }
}