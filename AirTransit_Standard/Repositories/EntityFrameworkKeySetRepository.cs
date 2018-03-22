using System.Linq;
using System.Security.Cryptography;
using AirTransit_Standard.Models;
using AirTransit_Standard.Utilities;

namespace AirTransit_Standard.Repositories
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
            Commit();
            return keySet;
        }

        private static KeySet CreateRSAKeyPair(string phoneNumber)
        {
            using (RSA rsa = RSA.Create())
            {
                var publicKey = rsa.ToXmlStringNetCore(false);
                var privateKey = rsa.ToXmlStringNetCore(true);
                return new KeySet(phoneNumber, publicKey, privateKey);
            }
        }
    }
}