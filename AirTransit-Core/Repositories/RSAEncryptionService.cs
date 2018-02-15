using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using AirTransit_Core.Models;
using Newtonsoft.Json;

namespace AirTransit_Core.Repositories
{
    class RSAEncryptionService : IEncryptionService
    {
        private static readonly RSAEncryptionPadding RsaEncryptionPadding = RSAEncryptionPadding.Pkcs1;

        public byte[] Encrypt(byte[] payload, string privateKey)
        {
            // TODO: Deserialize rsa parameters
            // var rsaParameters = JsonConvert.DeserializeObject<RSAParameters>(privateKey);
            // using (var rsa = RSA.Create(rsaParameters))
            // {
            //     return rsa.Encrypt(payload, RsaEncryptionPadding);
            // }
            throw new NotImplementedException();
        }

        public byte[] Decrypt(byte[] payload, string destinationPublicKey)
        {
            // TODO: Deserialize rsa parameters
            // var rsaParameters = JsonConvert.DeserializeObject<RSAParameters>(destinationPublicKey);
            // using (var rsa = RSA.Create(rsaParameters))
            // {
            //     return rsa.Decrypt(payload, RsaEncryptionPadding);
            // }
            throw new NotImplementedException();
        }
    }
}