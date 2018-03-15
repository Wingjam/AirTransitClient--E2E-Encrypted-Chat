using System.Linq;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Threading;
using AirTransit_Core.Models;
using Newtonsoft.Json;

namespace AirTransit_Core.Repositories
{
    internal class EntityFrameworkKeySetRepository : EntityFrameworkRepository, IKeySetRepository
    {
        private readonly string _clientPhoneNumber;

        public EntityFrameworkKeySetRepository(string clientPhoneNumber, MessagingContext messagingContext) : base(messagingContext)
        {
            this._clientPhoneNumber = clientPhoneNumber;
        }
        
        public KeySet GetKeySet()
        {
            return this.MessagingContext.KeySet.SingleOrDefault(ks => ks.PhoneNumber == this._clientPhoneNumber);
        }

        public KeySet CreateKeySet()
        {
            var keySet = CreateRSAKeyPair(this._clientPhoneNumber);
            MessagingContext.KeySet.Add(keySet);
            return keySet;
        }

        private static KeySet CreateRSAKeyPair(string phoneNumber)
        {
            using (var rsa = RSA.Create())
            {
                var publicKey = rsa.ToXmlString(false);
                var privateKey = rsa.ToXmlString(true);
                return new KeySet(phoneNumber, publicKey, privateKey);
            }
        }
    }
}