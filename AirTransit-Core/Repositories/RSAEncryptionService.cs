using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AirTransit_Core.Models;
using Newtonsoft.Json;

namespace AirTransit_Core.Repositories
{
    class RSAEncryptionService : IEncryptionService
    {
        private readonly IKeySetRepository _keySetRepository;
        private static readonly RSAEncryptionPadding RsaEncryptionPadding = RSAEncryptionPadding.Pkcs1;
        private readonly Encoding _encoding = Encoding.UTF8;

        public RSAEncryptionService(IKeySetRepository keySetRepository)
        {
            this._keySetRepository = keySetRepository;
        }
        
        public string Encrypt(string message, Contact contact)
        {
            byte[] messageBytes = _encoding.GetBytes(message);
            
            try
            {
                byte[] encryptedData;
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(contact.PublicKey);
                    encryptedData = rsa.Encrypt(messageBytes, RsaEncryptionPadding);
                }
                return _encoding.GetString(encryptedData);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    
        public string Decrypt(string encryptedMessage)
        {
            var key = this._keySetRepository.GetOrCreateKeySet();
            var encryptedMessageBytes = _encoding.GetBytes(encryptedMessage);
            
            try
            {
                byte[] decryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.FromXmlString(key.PrivateKey);
                    decryptedData = RSA.Decrypt(encryptedMessageBytes, RsaEncryptionPadding);
                }
                return _encoding.GetString(decryptedData);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
    
                return null;
            }
        }
    }
}