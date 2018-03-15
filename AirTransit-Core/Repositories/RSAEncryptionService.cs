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
        private readonly Encoding _encoding;
        private static readonly RSAEncryptionPadding RsaEncryptionPadding = RSAEncryptionPadding.Pkcs1;

        public RSAEncryptionService(IKeySetRepository keySetRepository, Encoding encoding)
        {
            this._keySetRepository = keySetRepository;
            this._encoding = encoding;
        }

        public string GenerateSignature(string content)
        {
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    var contentBytes = this._encoding.GetBytes(content);
                    var clientKey = this._keySetRepository.GetOrCreateKeySet();
                    rsa.FromXmlString(clientKey.PrivateKey);
                    var signature = rsa.SignData(contentBytes, new SHA1CryptoServiceProvider());
                    return this._encoding.GetString(signature);
                }
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        
        public bool VerifySignature(string dataToVerify, string signedData, Contact contact)
        {
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    var dataToVerifyBytes = this._encoding.GetBytes(dataToVerify);
                    var signedDataBytes = this._encoding.GetBytes(signedData);
                    var clientKey = contact.PublicKey;
                    rsa.FromXmlString(clientKey);
                    return rsa.VerifyData(dataToVerifyBytes, new SHA1CryptoServiceProvider(), signedDataBytes);
                }
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
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